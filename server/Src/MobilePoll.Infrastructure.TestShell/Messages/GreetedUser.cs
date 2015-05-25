using MobilePoll.Bus;

namespace MobilePoll.Infrastructure.TestShell.Messages
{
    public class GreetedUser : IEvent
    {
        public string Name { get; private set; }

        public GreetedUser(string name)
        {
            Guard.ParameterNotNullOrEmpty(name, "Name");

            Name = name;
        }
    }
}