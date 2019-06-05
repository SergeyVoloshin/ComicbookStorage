
namespace ComicbookStorage.Application.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Base;
    using Configuration;
    using Domain.Core.Entities;
    using Domain.OperationResults;
    using Domain.Services;
    using DTOs.Account;
    using Flurl;
    using Infrastructure.Localization;
    using Microsoft.IdentityModel.Tokens;

    public interface IAccountService : IService
    {
        Task<bool> IsUserEmailTaken(string email);

        Task<bool> IsUserNameTaken(string name);

        Task<UserModificationResult> CreateUser(CreateUserDto newUserDto);

        Task<EmailConfirmationResult> ConfirmEmail(string confirmationCode);

        Task<string> Authenticate(LogInDto authenticationRequest);
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

        public Task<bool> IsUserEmailTaken(string email)
        {
            return accountManager.IsUserEmailTaken(email);
        }

        public Task<bool> IsUserNameTaken(string name)
        {
            return accountManager.IsUserNameTaken(name);
        }

        public async Task<UserModificationResult> CreateUser(CreateUserDto newUserDto)
        {
            User newUser = new User(newUserDto.Email, newUserDto.Name, newUserDto.Password);
            var result = await accountManager.CreateUser(newUser);
            if (result == UserModificationResult.Success)
            {
                await emailManager.EnqueueEmailConfirmation(
                    newUser, 
                    LocalizedResources.ApplicationName,
                    Url.Combine(appConfiguration.BaseUrl, appConfiguration.ConfirmEmailUrl, newUser.ConfirmationCode));
            }

            return result;
        }

        public Task<EmailConfirmationResult> ConfirmEmail(string confirmationCode)
        {
            return accountManager.ConfirmEmail(confirmationCode);
        }

        public async Task<string> Authenticate(LogInDto authenticationRequest)
        {
            if (await accountManager.CheckCredentials(authenticationRequest.Email, authenticationRequest.Password))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, authenticationRequest.Email)
                };
                var credentials = new SigningCredentials(securityConfiguration.GetEncodingKey(), securityConfiguration.SigningAlgorithm);

                var jwtToken = new JwtSecurityToken(
                    securityConfiguration.Issuer,
                    securityConfiguration.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(securityConfiguration.AccessExpiration),
                    signingCredentials: credentials
                );
                return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }

            return null;
        }
    }
}
