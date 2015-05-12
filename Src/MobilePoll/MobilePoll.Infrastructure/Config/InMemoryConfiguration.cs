using MobilePoll.Infrastructure.Bus;
using MobilePoll.Infrastructure.InMemory;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Infrastructure.Serialization;

namespace MobilePoll.Infrastructure.Config
{
    public class InMemoryConfiguration : IConfigurationModule
    {
        public void Configure(IContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<JsonObjectSerializer>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<InMemoryUnitOfWork>(DependencyLifecycle.SingleInstance);
            
            containerBuilder.RegisterType<InMemoryBus>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<MessageDispatcher>(DependencyLifecycle.InstancePerUnitOfWork);
            
        }
    }
}