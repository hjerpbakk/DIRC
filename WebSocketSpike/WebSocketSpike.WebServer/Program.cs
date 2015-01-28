// <copyright file="Program.cs" company="DIPS ASA">
// </copyright>
// <summary>
//   This is the web server that hosts the web application.
// </summary>

using System;
using System.Reflection;
using Microsoft.Owin.Hosting;

namespace WebSocketSpike.WebServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string UrlServer = "http://+:8080/";
            using (WebApp.Start<Startup>(UrlServer))
            {
                Console.WriteLine("{0} running at {1}", Assembly.GetExecutingAssembly().GetName().Name, UrlServer);
                Console.ReadLine();
            }

            Console.WriteLine("Server stopped.");
        }
    }
}