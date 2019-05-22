
namespace ComicbookStorage.Application.Services
{
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

    public interface IAccountService : IService
    {
        Task<bool> IsUserEmailTaken(string email);

        Task<bool> IsUserNameTaken(string name);

        Task<UserModificationResult> CreateUser(CreateUserDto newUserDto);
    }

    public class AccountService : ServiceBase, IAccountService
    {
        private readonly IAccountManager accountManager;
        private readonly IEmailManager emailManager;
        private readonly IAppConfiguration appConfiguration;

        public AccountService(
            IAccountManager accountManager, 
            IEmailManager emailManager, 
            IAppConfiguration appConfiguration,
            IMapper mapper) : base(mapper)
        {
            this.accountManager = accountManager;
            this.emailManager = emailManager;
            this.appConfiguration = appConfiguration;
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
    }
}
