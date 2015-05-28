using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.Ioc;
using MobilePoll.Logging;

namespace MobilePoll.Environment
{
    [DebuggerNonUserCode, DebuggerStepThrough]
    internal static class ComponentScanner
    {
        private static readonly ILog Logger = LogFactory.BuildLogger(typeof (ComponentScanner));

        public static void Scan(IContainerBuilder containerBuilder)
        {
            using (var scanner = new AssemblyScanner())
            {
                ICollection<Type> handlerTypes = GetHandlerTypes(scanner, typeof(IHandleCommand<>))
                    .Union(GetHandlerTypes(scanner, typeof(IHandleEvent<>)))
                    .Distinct(new TypeEqualityComparer()).ToArray();

                RegisterTypes(containerBuilder, handlerTypes, DependencyLifecycle.InstancePerUnitOfWork);
            }
        }

        private static ICollection<Type> GetHandlerTypes(AssemblyScanner scanner, Type handlerType)
        {
            return
                scanner.Types.Where(
                    t => !t.IsAbstract &&
                        t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType))
                       .Distinct(new TypeEqualityComparer())
                       .ToArray();
        }

        private static void RegisterTypes(IContainerBuilder containerBuilder, IEnumerable<Type> types, DependencyLifecycle dependencyLifecycle)
        {
            foreach (var type in types)
            {
                Logger.Debug("Registering message handler class {0}", type.Name);
                containerBuilder.RegisterType(type, dependencyLifecycle);
            }
        }        
    }
}
