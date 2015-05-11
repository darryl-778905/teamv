namespace MobilePoll.Infrastructure.Bus
{
    public interface ILocalBus 
    {
        void Execute(ICommand command);
        void Raise(IEvent @event);
    }
}