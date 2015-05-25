using System;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.Infrastructure.Bus;
using MobilePoll.Infrastructure.Persistence;
using MobilePoll.Infrastructure.TestShell.DataModel;
using MobilePoll.Infrastructure.TestShell.Messages;
using MobilePoll.Persistence;

namespace MobilePoll.Infrastructure.TestShell.Handlers
{
    public class HelloUserHandler : IHandleCommand<SayHelloToUser>, IHandleEvent<GreetedUser>
    {
        private readonly ILocalBus bus;
        private readonly IRepository<GreetingLog> repository;

        public HelloUserHandler(ILocalBus bus, IRepositoryFactory repositoryFactory)
        {
            this.bus = bus;
            this.repository = repositoryFactory.GetRepository<GreetingLog>();
        }

        public void Execute(SayHelloToUser c)
        {
            Console.WriteLine("Hello user {0}", c.Name);         
            bus.Raise(new GreetedUser(c.Name));
        }

        public void When(GreetedUser e)
        {
            repository.Add(new GreetingLog
            {
                Name = "Adrian",
                OccuredAt = DateTime.Now
            });
        }
    }
}