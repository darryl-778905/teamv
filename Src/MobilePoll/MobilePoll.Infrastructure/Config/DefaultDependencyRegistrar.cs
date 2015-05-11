using MobilePoll.Infrastructure.Bus;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Infrastructure.Serialization;

namespace MobilePoll.Infrastructure.Config
{
    internal class DefaultDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<LocalBus>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<JsonObjectSerializer>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<MessageDispatcher>(DependencyLifecycle.InstancePerUnitOfWork);
        }
    }
}