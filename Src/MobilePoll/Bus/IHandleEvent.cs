namespace MobilePoll.Bus
{
    public interface IHandleEvent<in TEvent> where TEvent : class, IEvent
    {
        void When(TEvent e);
    }
}