
namespace ComicbookStorage.Infrastructure.Cryptography
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class PasswordEncryptionProvider
    {
        public static string GetMd5(string input)
        {
            using (MD5 md5Hash = new MD5CryptoServiceProvider())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public static string GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
            {
                csprng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string CreateHash(string password, string salt, int pbkdf2IterationCount, int hashLength)
        {
            byte[] hash = Pbkdf2(password, Convert.FromBase64String(salt), pbkdf2IterationCount, hashLength);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string password, string hash, string salt, int pbkdf2IterationCount)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hashBytes = Convert.FromBase64String(hash);
            byte[] testHash = Pbkdf2(password, saltBytes, pbkdf2IterationCount, hashBytes.Length);
            return SlowEquals(hashBytes, testHash);
        }

        public static string GenerateConfirmationCode(string id, int length)
        {
            return $"{GetMd5(id)}{GenerateRandomString(length)}";
        }

        private static string GenerateRandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    result.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return result.ToString();
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] Pbkdf2(string password, byte[] salt, int iterationCount, int hashLength)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                pbkdf2.IterationCount = iterationCount;
                return pbkdf2.GetBytes(hashLength);
            }
        }
    }
}
