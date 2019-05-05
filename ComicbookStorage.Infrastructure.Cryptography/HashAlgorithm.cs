
namespace ComicbookStorage.Infrastructure.Cryptography
{
    using System.Text;

    public static class HashAlgorithm
    {
        public static string GetMd5(string input)
        {
            System.Security.Cryptography.MD5 md5Hash = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
