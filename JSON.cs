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
        /// This class is used for both request and response and is used for client-server communication.
        /// </summary>
        public class HashData
        {
            /// <summary>
            /// Method. Some methods are hashing algorithms, some are encryption algorithms.
            /// </summary>
            public string method { get; set; }
            /// <summary>
            /// Base64 representation of contents.
            /// </summary>
            public string contents { get; set; }
            /// <summary>
            /// Only used if method is hashing, null otherwise. Base64 representation of salt.
            /// </summary>
            public string? salt { get; set; }
            /// <summary>
            /// Only used if method is encryption, null otherwise. Base64 representation of a key.
            /// </summary>
            public string? key { get; set; }
            /// <summary>
            /// Only used if method is encryption, null otherwise. True for encrypt, false for decrypt.
            /// </summary>
            public bool? operation { get; set; }
            /// <summary>
            /// Only used for responses. Shows the initial length of encrypted data. Requires for decryption later. Just a quality-of-life feature.
            /// </summary>
            public long dataLen { get; set; }

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
