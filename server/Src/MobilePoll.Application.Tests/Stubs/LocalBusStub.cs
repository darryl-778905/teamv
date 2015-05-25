using System.Collections.Generic;
using System.Linq;
using MobilePoll.Bus;

namespace MobilePoll.Application.Tests.Stubs
{
    public class LocalBusStub : ILocalBus
    {
        public ICollection<ICommand> ExecutedCommands { get; set; }
        public ICollection<IEvent> RaisedEvents { get; set; }

        public LocalBusStub()
        {
            ExecutedCommands = new List<ICommand>();
            RaisedEvents = new List<IEvent>();
        }

        public void Execute(ICommand command)
        {
            ExecutedCommands.Add(command);
        }

        public void Raise(IEvent @event)
        {
            RaisedEvents.Add(@event);
        }

        public bool EventTypeWasRaised<T>() where T : IEvent
        {
            return RaisedEvents.Any(e => e.GetType() == typeof (T));
        }

        public T GetFirstEventOfType<T>() where T : IEvent
        {
            return (T)RaisedEvents.First(e => e.GetType() == typeof(T));
        }
    }
}