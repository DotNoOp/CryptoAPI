using System;
using System.Text;
using System.Security.Cryptography;

namespace CryptoAPI
{
    class Crypto
    {
        public static string sha256(byte[] v)
        {
            using (SHA256 m = SHA256.Create())
            {
                return BitConverter.ToString(m.ComputeHash(v)).ToLower().Replace("-", "");
            }
        }
    }
}
