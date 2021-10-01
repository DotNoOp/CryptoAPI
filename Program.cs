using EmbedIO;
using EmbedIO.WebApi;
using System;
using System.Threading;

namespace CryptoAPI
{
    class Program
    {
        public static Config cfg = new Config("cfg.json");

        static void Main(string[] args)
        {
            Console.WriteLine($"-> Starting the server up at {cfg.url}...");

            CreateWebServer(cfg.url).RunAsync();

            while (true)
            {
                Thread.Sleep(10000);
            }
        }

        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                .WithCors()
                .WithWebApi("/", Controller.AsJSON, m => m
                    .WithController<Controller>());

            return server;
        }
    }
}
