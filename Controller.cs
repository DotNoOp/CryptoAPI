using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System;
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
            byte[] data = Convert.FromBase64String(p.contents);

            var response = new JSON.HashData();
            response.method = p.method;

            switch (p.method.ToLower())
            {
                case "md5":
                    response.contents = Crypto.md5(data);
                    break;
                case "sha1":
                case "sha128":
                    response.contents = Crypto.sha1(data);
                    break;
                case "sha512":
                    response.contents = Crypto.sha512(data);
                    break;
                case "sha2":
                case "sha256":
                default:
                    response.contents = Crypto.sha256(data);
                    break;
            }

            return JSON.Build(response);
        }
    }
}
