using Domain.Entities.Models.Masters;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace Application.Utils
{
    public static class PasswordHashUtil
    {
        public static string Key = "PYdQ9Xjc0LU5gH2XK6dDw1w+SYpJg2tPi5kNa+lsDAc=";
        public static string IV = "n3H4TvkLxyNDOsNGQ9NYzg==";
        public static bool VerifyPasswordHash(Users user, string hashedPassword, string inputPassword)
        {
            bool verified = true;
            if (!string.IsNullOrEmpty(hashedPassword))
            {
                var passwordHasher = new PasswordHasher<Users>();
                var result = passwordHasher.VerifyHashedPassword(user, hashedPassword, inputPassword);

                if (result is PasswordVerificationResult.Failed) verified = false;
            }
            else verified = false;

            return verified;
        }

        //public encrypt decrypt
        public static string EncryptString(string plainText)
        {
            if (Key == null || IV == null)
            {
                throw new InvalidOperationException("PasswordHashUtil is not initialized. Call Initialize() first.");
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);
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
                throw new InvalidOperationException("PasswordHashUtil is not initialized. Call Initialize() first.");
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);
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
