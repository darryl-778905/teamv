using System;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.Environment;
using MobilePoll.Infrastructure.Bus;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Infrastructure.Persistence;
using MobilePoll.Infrastructure.TestShell.DataModel;
using MobilePoll.Infrastructure.TestShell.Messages;
using MobilePoll.Infrastructure.Wireup;
using MobilePoll.Ioc;
using MobilePoll.Persistence;

namespace MobilePoll.Infrastructure.TestShell
{
    class Program
    {
        static void Main(string[] args)
        {
            ILocalBus bus = Intialize();

            var command = new SayHelloToUser(System.Environment.UserName);
            bus.Execute(command);

            using (var scope = Configuration.RootContainer.BeginLifetimeScope())
            {
                var repositoryFactory = scope.GetInstance<IRepositoryFactory>();

                var repository = repositoryFactory.GetRepository<GreetingLog>();

                var log = repository.Get(1);
                Console.WriteLine("Greeting log: Greeted {0} at {1}", log.Name, log.OccuredAt.ToShortTimeString());
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static ILocalBus Intialize()
        {
            IContainerBuilder containerBuilder = new AutofacAdapter();
            containerBuilder.RegisterConfigurationModule(new InMemoryConfiguration());
            Configuration.Initialize(containerBuilder);
            return Configuration.Bus;
        }
    }
}
