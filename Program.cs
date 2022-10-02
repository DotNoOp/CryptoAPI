using EmbedIO;
using EmbedIO.WebApi;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CryptoAPI
{
    class Program
    {
        public static Config cfg = Config.LoadConfig("cfg.json");

        static void Main(string[] args)
        {
            Console.WriteLine($"-> Starting the server up at {cfg.url}...");

            CreateWebServer(cfg.url).RunAsync();

            var tcp = new TcpListener(IPEndPoint.Parse(cfg.tcpConnection));
            tcp.Start();

            while (true)
            {
                var client = tcp.AcceptTcpClient();
                var ns = client.GetStream();

                var sw = new StreamWriter(ns); sw.AutoFlush = true;
                var sr = new StreamReader(ns);

                string json = sr.ReadLine();

                var p = JsonConvert.DeserializeObject<JSON.HashData>(json);

                MemoryStream dataStream = new MemoryStream();
                dataStream.Write(Convert.FromBase64String(p.contents));
                if (!string.IsNullOrEmpty(p.salt)) dataStream.Write(Convert.FromBase64String(p.salt));
                var data = dataStream.ToArray();

                var response = new JSON.HashData();
                response.method = p.method;
                response.salt = p.salt;
                response.key = p.key;
                response.operation = p.operation;
                response.dataLen = data.Length;

                switch (p.method.ToLower())
                {
                    //hashing
                    case "md5":
                        response.contents = Crypto.md5(data);
                        break;
                    case "sha1":
                    case "sha128":
                        response.contents = Crypto.sha1(data);
                        break;
                    case "sha2":
                    case "sha256":
                    default:
                        response.contents = Crypto.sha256(data);
                        break;
                    case "sha384":
                        response.contents = Crypto.sha384(data);
                        break;
                    case "sha512":
                        response.contents = Crypto.sha512(data);
                        break;
                    //encryption
                    case "aes256":
                        response.contents = Crypto.aes256(data, Convert.FromBase64String(p.key), p.operation);
                        break;
                    case "tripledes":
                    case "tdes":
                        response.contents = Crypto.TDES(data, Convert.FromBase64String(p.key), p.operation);
                        break;
                }

                sw.WriteLine(JSON.Build(response));
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
