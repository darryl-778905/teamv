using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;
using MobilePoll.Web.Api.Configuration;

namespace MobilePoll.Web.Api
{
    class Program
    {
        const string BaseAddress = "http://localhost:9000/";

        static void Main(string[] args)
        {
            Console.WriteLine("Configuring OWIN Self Host to run on {0}", BaseAddress);
            Console.WriteLine("Starting OWIN Server...");

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: BaseAddress))
            {
                Console.WriteLine("\nServer Started. To test the connection go to {0}api/test", BaseAddress);
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
