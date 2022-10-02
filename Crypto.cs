using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;

namespace CryptoAPI
{
    class Crypto
    {
        #region Hashing algorithms
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
        public static string sha384(byte[] v)
        {
            using (SHA384 m = SHA384.Create())
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
        #endregion

        #region Encryption algorithms
        public static string aes256(byte[] v, byte[] key, bool? operation)
        {
            var provider = new AesCryptoServiceProvider
            {
                KeySize = 256,
                BlockSize = 128,
                Key = key,
                Padding = PaddingMode.None,
                Mode = CipherMode.ECB
            };

            if (!operation.HasValue) operation = true;

            if ((bool)operation)
            {
                var enc = provider.CreateEncryptor();
                List<byte> te = v.ToList();
                while (te.Count % 16 != 0) te.Add(0x00);
                v = te.ToArray();
                byte[] resultArray = enc.TransformFinalBlock(v, 0, v.Length);
                return Convert.ToBase64String(resultArray);
            } else
            {
                var dec = provider.CreateDecryptor();
                byte[] resultArray = dec.TransformFinalBlock(v, 0, v.Length);
                return Convert.ToBase64String(resultArray);
            }
        }

        public static string TDES(byte[] v, byte[] key, bool? operation)
        {
            var K = key.ToList();
            while (K.Count < 24)
            {
                K.Add(0x00);
            }
            key = K.ToArray();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider()
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.Zeros
            };

            if (!operation.HasValue) operation = true;

            if ((bool)operation)
            {
                var enc = tdes.CreateEncryptor();
                byte[] resultArray = enc.TransformFinalBlock(v, 0, v.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray);
            }
            else
            {
                var dec = tdes.CreateDecryptor();
                byte[] resultArray = dec.TransformFinalBlock(v, 0, v.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray);
            }
        }
        #endregion
    }
}
