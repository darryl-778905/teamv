using MobilePoll.Infrastructure.Bus;
using MobilePoll.Infrastructure.Persistence.Mongo;
using MobilePoll.Infrastructure.Serialization;
using MobilePoll.Ioc;

namespace MobilePoll.Infrastructure.Wireup
{
    public class MongoConfiguration : IConfigurationModule
    {
        public void Configure(IContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<JsonObjectSerializer>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<MongoUnitOfWork>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<MessageDispatcher>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<InMemoryBus>(DependencyLifecycle.SingleInstance);
        }
    }
}