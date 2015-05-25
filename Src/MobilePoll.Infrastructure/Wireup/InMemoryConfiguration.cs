using MobilePoll.Infrastructure.Bus;
using MobilePoll.Infrastructure.Persistence;
using MobilePoll.Infrastructure.Serialization;
using MobilePoll.Ioc;

namespace MobilePoll.Infrastructure.Wireup
{
    public class InMemoryConfiguration : IConfigurationModule
    {
        public void Configure(IContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<JsonObjectSerializer>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<InMemoryUnitOfWork>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<MessageDispatcher>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<InMemoryBus>(DependencyLifecycle.SingleInstance);
        }
    }
}