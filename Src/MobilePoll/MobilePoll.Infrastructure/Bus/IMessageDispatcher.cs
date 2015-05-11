using Microsoft.Practices.ServiceLocation;

namespace MobilePoll.Infrastructure.Bus
{
    public interface IMessageDispatcher
    {
        void DispatchToHandlers(object message, IServiceLocator serviceLocator);
    }
}