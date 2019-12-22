
namespace ComicbookStorage.Application.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using Base;
    using Configuration;
    using Domain.Core.Entities;
    using Domain.OperationResults;
    using Domain.Services;
    using DTOs.Account;
    using Flurl;
    using Microsoft.IdentityModel.Tokens;

    public interface IAccountService : IService
    {
        Task<bool> IsUserEmailTaken(string email, string userEmail);

        Task<bool> IsUserNameTaken(string name, string userEmail);

        Task<UserModificationResult> CreateUser(CreateUserDto newUserDto);

        Task<UserModificationResult> UpdateUser(string email, UpdateUserDto updateUserDto);

        Task<EmailConfirmationResult> ConfirmEmail(string confirmationCode);

        Task<RestoreAccessResult> RestoreAccess(RestoreAccessDto restoreRequest);

        Task<AuthenticationResponseDto> Authorize(LogInDto authenticationRequest, string userAgent);

        Task<AuthenticationResponseDto> RefreshToken(RefreshTokenDto tokens, string userAgent);

        Task<UserDataDto> GetUserData(string userEmail);
    }

    public class AccountService : ServiceBase, IAccountService
    {
        private readonly IAccountManager accountManager;
        private readonly IEmailManager emailManager;
        private readonly IAppConfiguration appConfiguration;
        private readonly ISecurityConfiguration securityConfiguration;

        public AccountService(
            IAccountManager accountManager, 
            IEmailManager emailManager, 
            IAppConfiguration appConfiguration,
            ISecurityConfiguration securityConfiguration,
            IMapper mapper) : base(mapper)
        {
            this.accountManager = accountManager;
            this.emailManager = emailManager;
            this.appConfiguration = appConfiguration;
            this.securityConfiguration = securityConfiguration;
        }

        public Task<bool> IsUserEmailTaken(string email, string userEmail)
        {
            return accountManager.IsUserEmailTaken(email, userEmail);
        }

        public Task<bool> IsUserNameTaken(string name, string userEmail)
        {
            return accountManager.IsUserNameTaken(name, userEmail);
        }

        public async Task<UserModificationResult> CreateUser(CreateUserDto newUserDto)
        {
            User newUser = new User(newUserDto.Email, newUserDto.Name, newUserDto.Password);
            var result = await accountManager.CreateUser(newUser);
            if (result == UserModificationResult.SuccessConfirmationRequired)
            {
                await emailManager.EnqueueEmailConfirmation(
                    newUser, 
                    GetEmailConfirmationUrl(newUser.ConfirmationCode));
            }

            return result;
        }

        public async Task<UserModificationResult> UpdateUser(string email, UpdateUserDto updateUserDto)
        {
            (UserModificationResult result, User user) = await accountManager.UpdateUser(
                email, 
                updateUserDto.Email, 
                updateUserDto.Name, 
                updateUserDto.NewPassword, 
                updateUserDto.OldPassword);

            if (result == UserModificationResult.SuccessConfirmationRequired)
            {
                await emailManager.EnqueueEmailConfirmation(
                    user,
                    GetEmailConfirmationUrl(user.ConfirmationCode));
            }

            return result;
        }

        public Task<EmailConfirmationResult> ConfirmEmail(string confirmationCode)
        {
            return accountManager.ConfirmEmail(confirmationCode);
        }

        public async Task<RestoreAccessResult> RestoreAccess(RestoreAccessDto restoreRequest)
        {
            var (result, user, newPassword) = await accountManager.RestoreAccess(restoreRequest.Email);
            switch (result)
            {
                case RestoreAccessResult.ResendConfirmationCode:
                    await emailManager.EnqueueEmailConfirmation(user, GetEmailConfirmationUrl(user.ConfirmationCode));
                    break;
                case RestoreAccessResult.ResetPassword:
                    await emailManager.EnqueuePasswordResetEmail(user, newPassword, Url.Combine(appConfiguration.BaseUrl, appConfiguration.LogInUrl));
                    break;
            }

            return result;
        }

        public async Task<AuthenticationResponseDto> Authorize(LogInDto authenticationRequest, string userAgent)
        {
            string refreshToken = await accountManager.Authorize(authenticationRequest.Email, authenticationRequest.Password, userAgent, securityConfiguration.RefreshExpiration);
            return GetAuthenticationResponse(authenticationRequest.Email, refreshToken);
        }

        public async Task<AuthenticationResponseDto> RefreshToken(RefreshTokenDto tokens, string userAgent)
        {
            var principal = GetPrincipalFromExpiredToken(tokens.AccessToken);
            Claim claim = principal?.FindFirst(c => c.Type == ClaimTypes.Email);
            if (claim != null)
            {
                string renewedRefreshToken = await accountManager.RefreshToken(claim.Value, tokens.RefreshToken, userAgent, securityConfiguration.RefreshExpiration);
                return GetAuthenticationResponse(claim.Value, renewedRefreshToken);
            }

            return null;
        }

        public async Task<UserDataDto> GetUserData(string userEmail)
        {
            var user = await accountManager.GetUserData(userEmail);
            return Mapper.Map<User, UserDataDto>(user);
        }

        private AuthenticationResponseDto GetAuthenticationResponse(string email, string refreshToken)
        {
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, email)
                };
                var credentials = new SigningCredentials(securityConfiguration.GetEncodingKey(), securityConfiguration.SigningAlgorithm);

                var jwtToken = new JwtSecurityToken(
                    securityConfiguration.Issuer,
                    securityConfiguration.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(securityConfiguration.AccessExpiration),
                    signingCredentials: credentials
                );
                string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return new AuthenticationResponseDto(accessToken, refreshToken);
            }

            return null;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = securityConfiguration.GeTokenValidationParameters();
            tokenValidationParameters.ValidateLifetime = false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is JwtSecurityToken jwtSecurityToken &&
                jwtSecurityToken.Header.Alg.Equals(securityConfiguration.SigningAlgorithm, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }

            return null;
        }

        private string GetEmailConfirmationUrl(string confirmationCode)
        {
            return Url.Combine(appConfiguration.BaseUrl, appConfiguration.ConfirmEmailUrl, confirmationCode);
        }
    }
}
