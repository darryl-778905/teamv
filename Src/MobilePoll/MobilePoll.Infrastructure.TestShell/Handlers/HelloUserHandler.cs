using System;
using MobilePoll.Infrastructure.Bus;
using MobilePoll.Infrastructure.TestShell.Messages;

namespace MobilePoll.Infrastructure.TestShell.Handlers
{
    public class HelloUserHandler : IHandleCommand<SayHelloToUser>, IHandleEvent<GreetedUser>
    {
        private readonly ILocalBus bus;

        public HelloUserHandler(ILocalBus bus)
        {
            this.bus = bus;
        }

        public void Execute(SayHelloToUser c)
        {
            Console.WriteLine("Hello user {0}", c.Name);
            bus.Raise(new GreetedUser(c.Name));
        }

        public void When(GreetedUser e)
        {
            Console.WriteLine("User {0} was greeted", e.Name);
        }
    }
}