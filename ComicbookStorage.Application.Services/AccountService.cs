
namespace ComicbookStorage.Application.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Base;
    using Domain.OperationResults;
    using Domain.Services;
    using DTOs.Account;

    public interface IAccountService : IService
    {
        Task<bool> IsUserEmailTaken(string email);

        Task<bool> IsUserNameTaken(string name);

        Task<UserModificationResult> CreateUser(CreateUserDto newUser);
    }

    public class AccountService : ServiceBase, IAccountService
    {
        private readonly IAccountManager accountManager;

        public AccountService(IAccountManager accountManager, IMapper mapper) : base(mapper)
        {
            this.accountManager = accountManager;
        }

        public Task<bool> IsUserEmailTaken(string email)
        {
            return accountManager.IsUserEmailTaken(email);
        }

        public Task<bool> IsUserNameTaken(string name)
        {
            return accountManager.IsUserNameTaken(name);
        }

        public Task<UserModificationResult> CreateUser(CreateUserDto newUser)
        {
            return accountManager.CreateUser(newUser.Email, newUser.Name, newUser.Password);
        }
    }
}
