namespace MobilePoll.Infrastructure
{
    public interface ILocalBus 
    {
        void Execute(ICommand command);
        void Raise(IEvent @event);
    }
}