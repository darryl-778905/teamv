using System;
using System.Collections.Generic;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.Ioc;

namespace MobilePoll.Environment
{
    internal static class ComponentScanner
    {
        public static void Scan(IContainerBuilder containerBuilder)
        {
            using (var scanner = new AssemblyScanner())
            {
                ICollection<Type> commandHandlerTypes = GetHandlerTypes(scanner, typeof(IHandleCommand<>));
                ICollection<Type> eventHandlerTypes = GetHandlerTypes(scanner, typeof(IHandleEvent<>));

                RegisterTypes(containerBuilder, commandHandlerTypes, DependencyLifecycle.InstancePerUnitOfWork);
                RegisterTypes(containerBuilder, eventHandlerTypes, DependencyLifecycle.InstancePerUnitOfWork);
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
                containerBuilder.RegisterType(type, dependencyLifecycle);
            }
        }        
    }
}
