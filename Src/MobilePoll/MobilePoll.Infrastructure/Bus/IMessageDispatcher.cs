using Microsoft.Practices.ServiceLocation;

namespace MobilePoll.Infrastructure
{
    public interface IMessageDispatcher
    {
        void DispatchToHandlers(object message, IServiceLocator serviceLocator);
    }
}