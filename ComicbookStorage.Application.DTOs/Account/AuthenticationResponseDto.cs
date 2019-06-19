
namespace ComicbookStorage.Application.DTOs.Account
{
    public class AuthenticationResponseDto
    {
        public AuthenticationResponseDto(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
