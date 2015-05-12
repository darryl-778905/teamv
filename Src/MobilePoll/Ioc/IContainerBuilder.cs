using System;

namespace MobilePoll.Ioc
{
    public interface IContainerBuilder : IDisposable
    {
        IServiceContainer BuildContainer();
        void RegisterType<T>(DependencyLifecycle dependencyLifecycle);
        void RegisterType<T>();
        void RegisterType(Type type, DependencyLifecycle dependencyLifecycle);
        void RegisterType(Type type);
        void RegisterSingleton(object instance);
        void RegisterConfigurationModule(IConfigurationModule module);
    }
}