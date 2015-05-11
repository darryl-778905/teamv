namespace MobilePoll.Infrastructure.Ioc
{
    public interface IDependencyRegistrar
    {
        void Register(IContainerBuilder containerBuilder);
    }
}