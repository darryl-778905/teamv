using System;
using Microsoft.Owin.Hosting;
using MobilePoll.Infrastructure.Persistence.Mongo;
using MobilePoll.Infrastructure.Wireup;
using MobilePoll.Web.Api.Wireup;

namespace MobilePoll.Web.Api
{
    class Program
    {
        private static readonly string BaseAddress = String.Format("http://{0}:9000", System.Environment.MachineName);

        static void Main(string[] args)
        {
            MongoUnitOfWork.DropDatabaseOnStartup = false;
            Startup.DefaultConfiguration = new MongoConfiguration();

            Console.WriteLine("Configuring OWIN Self Host to run on {0}", BaseAddress);
            Console.WriteLine("Starting OWIN Server with {0} configuration...", Startup.DefaultConfiguration.GetType().Name);

            Console.WriteLine();

            StartOptions options = new StartOptions();
            options.Urls.Add("http://localhost:9000");
            options.Urls.Add("http://127.0.0.1:9000");
            options.Urls.Add("http://192.168.0.3:9000");
            options.Urls.Add(BaseAddress);

            // Start OWIN host 
            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("\nServer Started. To test the connection go to {0}api/Test", BaseAddress);

                Console.WriteLine("\nAvailable API Calls:");
                Console.WriteLine("GET {0}api/PollResult", BaseAddress);
                Console.WriteLine("GET {0}api/PollResult/{{id}}", BaseAddress);
                Console.WriteLine("POST {0}api/PollResult", BaseAddress);
                Console.WriteLine();
                Console.WriteLine("GET {0}api/PollResult", BaseAddress);
                Console.WriteLine("GET {0}api/PollResult/{{id}}", BaseAddress);
                Console.WriteLine("POST {0}api/PollResult", BaseAddress);
                Console.WriteLine();
                Console.WriteLine("GET {0}api/Report", BaseAddress);

                Console.WriteLine("\nPress the X key to stop the server.");

                do
                {
                    ConsoleKeyInfo keyPress = Console.ReadKey();

                    if (keyPress.KeyChar == 'x' || keyPress.KeyChar == 'X')
                    {
                        break;
                    }
                } while (true);
            }
        }
    }
}
