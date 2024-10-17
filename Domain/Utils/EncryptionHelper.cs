using Domain.Entities.Models.Masters;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils
{
    public static class EncryptionHelper
    {
        public static string Key = "PYdQ9Xjc0LU5gH2XK6dDw1w+SYpJg2tPi5kNa+lsDAc=";
        public static string IV = "n3H4TvkLxyNDOsNGQ9NYzg==";
        public static string EncryptString(string plainText)
        {
            if (Key == null || IV == null)
            {
                throw new InvalidOperationException("EncryptionHelper is not initialized.");
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(Key);
                aes.IV = Convert.FromBase64String(IV);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        public static string DecryptString(string cipherText)
        {
            if (Key == null || IV == null)
            {
                throw new InvalidOperationException("EncryptionHelper is not initialized.");
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(Key);
                aes.IV = Convert.FromBase64String(IV);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
