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
        public static string md5(byte[] v)
        {
            using (MD5 m = MD5.Create())
            {
                return BitConverter.ToString(m.ComputeHash(v)).ToLower().Replace("-", "");
            }
        }
        public static string sha1(byte[] v)
        {
            using (SHA1 m = SHA1.Create())
            {
                return BitConverter.ToString(m.ComputeHash(v)).ToLower().Replace("-", "");
            }
        }
        public static string sha512(byte[] v)
        {
            using (SHA512 m = SHA512.Create())
            {
                return BitConverter.ToString(m.ComputeHash(v)).ToLower().Replace("-", "");
            }
        }
    }
}
