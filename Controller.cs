using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
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
            return Crypto.sha256(val);
        }
    }
}
