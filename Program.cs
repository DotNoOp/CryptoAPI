using EmbedIO;
using EmbedIO.WebApi;
using System;
using System.Threading;

namespace CryptoAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CreateWebServer("http://127.0.0.1:80/").RunAsync();

            while (true)
            {
                Thread.Sleep(100);
            }
        }

        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                .WithCors()
                .WithWebApi("/", Controller.AsText, m => m
                    .WithController<Controller>());

            return server;
        }
    }
}
