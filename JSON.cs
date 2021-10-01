using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAPI
{
    class JSON
    {
        public class HashData
        {
            public string method { get; set; }
            public string contents { get; set; }
            public string? salt { get; set; }

            public HashData()
            {

            }
            public HashData(string method, string contents)
            {
                this.method = method;
                this.contents = contents;
            }
        }

        public static string Build(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
