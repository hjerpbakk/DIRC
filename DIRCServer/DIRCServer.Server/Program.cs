using System;
using System.Reflection;
using Microsoft.Owin.Hosting;

namespace DIRCServer.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string UrlWebServer = "http://*:14507/";

            using (WebApp.Start<Startup>(UrlWebServer))
            {
                Console.WriteLine("{0} running at {1}", Assembly.GetExecutingAssembly().GetName().Name, UrlWebServer);
                Console.ReadLine();
            }

            Console.WriteLine("Server stopped.");
        }
    }
}
