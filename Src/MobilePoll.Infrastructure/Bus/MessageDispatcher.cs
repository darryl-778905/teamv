using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.ServiceLocation;
using MobilePoll.Bus;

namespace MobilePoll.Infrastructure.Bus
{
    [DebuggerNonUserCode, DebuggerStepThrough]
    internal class MessageDispatcher : IMessageDispatcher
    {
        private static readonly Type CommandHandlerGenericType = typeof(IHandleCommand<>);
        private static readonly Type EventHandlerGenericType = typeof(IHandleEvent<>);

        public void DispatchToHandlers(object message, IServiceLocator serviceLocator)
        {
            Guard.ParameterNotNull(message, "message");
            Type messageType = message.GetType();

            if (IsCommand(messageType))
            {
                DispatchCommandToHandler(message, serviceLocator, messageType);
            }
            else if (IsEvent(messageType))
            {
                DispatchEventToHandlers(message, serviceLocator, messageType);
            }
            else
            {
                ThrowInvalidMessageTypeException(messageType);
            }
        }

        private static bool IsCommand(Type type)
        {
            return typeof(ICommand).IsAssignableFrom(type);
        }

        private static bool IsEvent(Type type)
        {
            return typeof(IEvent).IsAssignableFrom(type);
        }

        private static void DispatchCommandToHandler(object message, IServiceLocator serviceLocator, Type messageType)
        {
            Type commandHandlerType = CommandHandlerGenericType.MakeGenericType(messageType);
            object commandHandler = serviceLocator.GetInstance(commandHandlerType);
            ((dynamic)commandHandler).Execute((dynamic)message);
        }

        private static void DispatchEventToHandlers(object message, IServiceLocator serviceLocator, Type messageType)
        {
            Type eventHandlerType = EventHandlerGenericType.MakeGenericType(messageType);
            IEnumerable<object> eventHandlers = serviceLocator.GetAllInstances(eventHandlerType);

            foreach (var eventHandler in eventHandlers)
            {
                ((dynamic)eventHandler).When((dynamic)message);
            }
        }

        private static void ThrowInvalidMessageTypeException(Type messageType)
        {
            string errorMessage = String.Format("Type {0} is not a valid message type. Ensure that your message either implements ICommand or IEvent",
                messageType.FullName);

            throw new InvalidMessageTypeException(errorMessage);
        }        
    }
}