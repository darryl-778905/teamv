using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core.Registration;
using Microsoft.Practices.ServiceLocation;
using MobilePoll.Ioc;
using MobilePoll.Logging;

namespace MobilePoll.Infrastructure.Ioc
{
    [DebuggerNonUserCode, DebuggerStepThrough]
    public class AutofacAdapter : ServiceLocatorImplBase, IContainerBuilder, IServiceContainer
    {
        private static readonly ILog Logger = LogFactory.BuildLogger(typeof(AutofacAdapter));

        protected readonly ILifetimeScope LifetimeScope;
        private bool disposed;

        public AutofacAdapter()
            : this(null)
        {
        }

        public AutofacAdapter(ILifetimeScope container)
        {
            if (container == null)
            {
                LifetimeScope = new ContainerBuilder().Build();
            }
            else
            {
                LifetimeScope = container;
                Logger.Debug("Starting new lifetime scope {0}", LifetimeScope.GetHashCode());
            }
        }

        ~AutofacAdapter()
        {
            Dispose(false);
        }

        public IServiceContainer BuildContainer()
        {
            return this;
        }

        public virtual IServiceContainer BeginLifetimeScope()
        {
            return new AutofacAdapter(LifetimeScope.BeginLifetimeScope());
        }

        public void RegisterConfigurationModule(IConfigurationModule module)
        {
            module.Configure(this);
        }

        public void RegisterSingleton(object instance)
        {
            if (IsComponentAlreadyRegistered(instance.GetType()))
            {
                return;
            }

            var services = GetAllServices(instance.GetType());
            var builder = new ContainerBuilder();

            builder.RegisterInstance(instance).As(services)
                .PropertiesAutowired();

            builder.Update(LifetimeScope.ComponentRegistry);
        }

        public void RegisterType<T>()
        {
            RegisterType(typeof(T));
        }

        public void RegisterType(Type type)
        {
            RegisterType(type, DependencyLifecycle.InstancePerUnitOfWork);
        }

        public void RegisterType(Type type, DependencyLifecycle dependencyLifecycle)
        {
            if (IsComponentAlreadyRegistered(type))
            {
                return;
            }

            var services = GetAllServices(type);

            var builder = new ContainerBuilder();
            var registration = builder.RegisterType(type)
                .As(services)
                .PropertiesAutowired();

            ConfigureLifetimeScope(dependencyLifecycle, registration);
            builder.Update(LifetimeScope.ComponentRegistry);
        }

        public void RegisterType<T>(DependencyLifecycle dependencyLifecycle)
        {
            ((IContainerBuilder)this).RegisterType(typeof(T), dependencyLifecycle);
        }

        protected virtual void ConfigureLifetimeScope<T>(DependencyLifecycle dependencyLifecycle, IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration)
        {
            switch (dependencyLifecycle)
            {
                case DependencyLifecycle.SingleInstance:
                    registration.SingleInstance();
                    break;
                case DependencyLifecycle.InstancePerDependency:
                    registration.InstancePerDependency();
                    break;
                case DependencyLifecycle.InstancePerUnitOfWork:
                    registration.InstancePerLifetimeScope();
                    break;
                default:
                    throw new ArgumentException("Unknown container lifecycle - " + dependencyLifecycle);
            }
        }

        static Type[] GetAllServices(Type type)
        {
            if (type == null)
            {
                return new Type[0];
            }

            var result = new List<Type>(type.GetInterfaces()) {
                type
            };

            foreach (Type interfaceType in type.GetInterfaces())
            {
                result.AddRange(GetAllServices(interfaceType));
            }

            return result.Distinct().ToArray();
        }

        private bool IsComponentAlreadyRegistered(Type concreteComponent)
        {
            return LifetimeScope.ComponentRegistry.Registrations.FirstOrDefault(x => x.Activator.LimitType == concreteComponent) != null;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            Guard.ParameterNotNull(serviceType, "serviceType");

            try
            {
                return key != null
                    ? LifetimeScope.ResolveNamed(key, serviceType)
                    : LifetimeScope.Resolve(serviceType);
            }
            catch (ComponentNotRegisteredException ex)
            {
                throw new ComponentRegistrationException(ex.Message, ex);
            }
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            Guard.ParameterNotNull(serviceType, "serviceType");

            try
            {
                var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
                object instance = LifetimeScope.Resolve(enumerableType);
                return ((IEnumerable)instance).Cast<object>().ToArray();
            }
            catch (ComponentNotRegisteredException ex)
            {
                throw new ComponentRegistrationException(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing && LifetimeScope != null)
            {
                Logger.Debug("Disposing lifetime scope {0}", GetHashCode());
                LifetimeScope.Dispose();
            }

            disposed = true;
        }

        public override int GetHashCode()
        {
            return LifetimeScope.GetHashCode();
        }
    }
}