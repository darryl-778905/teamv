using MobilePoll.Infrastructure.Bus;

namespace MobilePoll.Infrastructure.TestShell.Messages
{
    public class SayHelloToUser : ICommand
    {
        public string Name { get; private set; }

        public SayHelloToUser(string name)
        {
            Guard.ParameterNotNullOrEmpty(name, "Name");

            Name = name;
        }
    }
}