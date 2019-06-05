
namespace ComicbookStorage.Application.Services.Configuration
{
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public interface ISecurityConfiguration
    {
        string Secret { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        int AccessExpiration { get; set; }
        int RefreshExpiration { get; set; }
        bool RequireHttpsMetadata { get; set; }
        string SigningAlgorithm { get; }
        SecurityKey GetEncodingKey();
        SecurityKey GetDecodingKey();
    }

    public class SecurityConfiguration : ISecurityConfiguration
    {
        private SymmetricSecurityKey symmetricKey;

        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessExpiration { get; set; }
        public int RefreshExpiration { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public string SigningAlgorithm => SecurityAlgorithms.HmacSha256;

        public SecurityKey GetEncodingKey()
        {
            if (symmetricKey == null)
            {
                symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            }

            return symmetricKey;
        }

        public SecurityKey GetDecodingKey()
        {
            return GetEncodingKey();
        }
    }
}
