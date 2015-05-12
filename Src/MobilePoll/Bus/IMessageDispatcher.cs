using Microsoft.Practices.ServiceLocation;

namespace MobilePoll.Bus
{
    public interface IMessageDispatcher
    {
        void DispatchToHandlers(object message, IServiceLocator serviceLocator);
    }
}