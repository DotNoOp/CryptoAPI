using System;
using System.Text;
using System.Security.Cryptography;

namespace CryptoAPI
{
    class Crypto
    {
        public static string sha256(string v)
        {
            using (SHA256 m = SHA256.Create())
            {
                return BitConverter.ToString(m.ComputeHash(Encoding.UTF8.GetBytes(v))).ToLower().Replace("-", "");
            }
        }
    }
}
