using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        [Route(EmbedIO.HttpVerbs.Get, "/{val?}")]
        public async Task<string> Hash(string val)
        {
            return md5(val);
        }

        public static string md5(string v)
        {
            using (MD5 m = MD5.Create())
            {
                return BitConverter.ToString(m.ComputeHash(Encoding.UTF8.GetBytes(v))).ToLower().Replace("-", "");
            }
        }
    }
}
