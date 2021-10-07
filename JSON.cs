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
        /// <summary>
        /// This class is used for both request and response and contains hashing method, salt and data (either raw for request or processed for response).
        /// </summary>
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

        /// <summary>
        /// This function builds a JSON string out of object
        /// </summary>
        /// <param name="o">Object to build a JSON string out of</param>
        /// <returns>JSON string</returns>
        public static string Build(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
