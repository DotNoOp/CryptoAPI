using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAPI
{
    class Controller : WebApiController
    {
        public static async Task AsJSON(IHttpContext context, object? data)
        {
            if (data is null)
            {
                // Send an empty response
                return;
            }

            context.Response.ContentType = MimeType.Json;
            using var text = context.OpenResponseText(Encoding.UTF8);
            // string.ToString returns the string itself
            await text.WriteAsync((string)data).ConfigureAwait(false);
        }

        [Route(EmbedIO.HttpVerbs.Post, "/")]
        public async Task<string> Hash()
        {
            var p = await HttpContext.GetRequestDataAsync<JSON.HashData>();

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

            return JSON.Build(response);
        }
    }
}
