// <copyright file="Program.cs" company="DIPS ASA">
// </copyright>
// <summary>
//   This is the local web service running on the local device.
// </summary>

using System;
using System.Reflection;
using Microsoft.Owin.Hosting;

namespace WebSocketSpike.LocalWebServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string UrlWebServer = "http://*:1337/";

            using (WebApp.Start<Startup>(UrlWebServer))
            {
                Console.WriteLine("{0} running at {1}", Assembly.GetExecutingAssembly().GetName().Name, UrlWebServer);
                Console.ReadLine();
            }

            Console.WriteLine("Server stopped.");
        }
    }
}
