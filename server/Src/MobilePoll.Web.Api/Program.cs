﻿using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;
using MobilePoll.Web.Api.Wireup;

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
                Console.WriteLine("\nServer Started. To test the connection go to {0}api/Test", BaseAddress);

                Console.WriteLine("\nAvailable API Calls:");
                Console.WriteLine("GET {0}api/PollResult", BaseAddress);
                Console.WriteLine("GET {0}api/PollResult/{{id}}", BaseAddress);
                Console.WriteLine("POST {0}api/PollResult", BaseAddress);
                Console.WriteLine();
                Console.WriteLine("GET {0}api/PollResult", BaseAddress);
                Console.WriteLine("GET {0}api/PollResult/{{id}}", BaseAddress);
                Console.WriteLine("POST {0}api/PollResult", BaseAddress);

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
