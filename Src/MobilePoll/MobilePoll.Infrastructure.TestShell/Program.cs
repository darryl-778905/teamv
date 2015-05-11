using System;
using System.Runtime.InteropServices.ComTypes;
using MobilePoll.Infrastructure.Bus;
using MobilePoll.Infrastructure.Config;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Infrastructure.TestShell.Messages;
using MobilePoll.Infrastructure.TestShell.Stubs;
using MobilePoll.Infrasturcture.Autofac;

namespace MobilePoll.Infrastructure.TestShell
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainerBuilder containerBuilder = new AutofacAdapter();
            containerBuilder.RegisterType<UnitOfWorkStub>(DependencyLifecycle.SingleInstance);
            Configuration.Initialize(containerBuilder);

            ILocalBus bus = Configuration.Bus;
            var command = new SayHelloToUser(Environment.UserName);
            bus.Execute(command);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
