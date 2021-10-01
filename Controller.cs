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
        public static async Task AsText(IHttpContext context, object? data)
        {
            if (data is null)
            {
                // Send an empty response
                return;
            }

            context.Response.ContentType = MimeType.PlainText;
            using var text = context.OpenResponseText(Encoding.UTF8);
            // string.ToString returns the string itself
            await text.WriteAsync((string)data).ConfigureAwait(false);
        }

        [Route(EmbedIO.HttpVerbs.Post, "/")]
        public async Task<string> Hash()
        {
            var p = await HttpContext.GetRequestDataAsync<JSON.HashData>();

            byte[] data = Convert.FromBase64String(p.contents);

            switch (p.method.ToLower())
            {
                case "md5":
                    return Crypto.md5(data);
                case "sha1":
                case "sha128":
                    return Crypto.sha1(data);
                case "sha512":
                    return Crypto.sha512(data);
                case "sha2":
                case "sha256":
                default:
                    return Crypto.sha256(data);
            }

            
        }
    }
}
