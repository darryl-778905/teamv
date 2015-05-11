namespace MobilePoll.Infrastructure.Ioc
{
    public enum DependencyLifecycle
    {
        SingleInstance,
        InstancePerDependency,
        InstancePerUnitOfWork
    }
}