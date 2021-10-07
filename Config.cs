using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAPI
{
    public class Config
    {
        public string url = "http://127.0.0.1:80/";

        public bool enableTCP = true;

        public string tcpConnection = "127.0.0.1:6631";

        #region Service functions
        public static Config LoadConfig(string filepath)
        {
            if (File.Exists(filepath))
            {
                return JsonConvert.DeserializeObject<Config>(File.ReadAllText(filepath));
            }
            else
            {
                Config cfg = new Config();
                cfg.SaveConfig(filepath);
                return cfg;
            }
        }

        public void SaveConfig(string filepath)
        {
            File.WriteAllText(filepath, JsonConvert.SerializeObject(this));
        }

        public Config()
        {

        }
        public Config(string filepath)
        {
            this.url = LoadConfig(filepath).url;
        }
        #endregion
    }
}
