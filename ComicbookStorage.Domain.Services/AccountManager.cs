
namespace ComicbookStorage.Domain.Services
{
    using System.Data;
    using System.Threading.Tasks;
    using Base;
    using Core.Entities;
    using Core.Entities.Specifications.User;
    using DataAccess;
    using DataAccess.Repositories;
    using OperationResults;

    public interface IAccountManager : IManager
    {
        Task<bool> IsUserEmailTaken(string email);

        Task<bool> IsUserNameTaken(string name);

        Task<UserModificationResult> CreateUser(User newUser);

        Task<EmailConfirmationResult> ConfirmEmail(string confirmationCode);

        Task<bool> CheckCredentials(string email, string password);
    }

    public class AccountManager : ManagerBase, IAccountManager
    {
        private readonly IUserRepository userRepository;

        public AccountManager(IUserRepository userRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.userRepository = userRepository;
        }

        public Task<bool> IsUserEmailTaken(string email)
        {
            return userRepository.ExistsAsync(new UserWithEmailSpec(email));
        }

        public Task<bool> IsUserNameTaken(string name)
        {
            return userRepository.ExistsAsync(new UserWithNameSpec(name));
        }

        public async Task<UserModificationResult> CreateUser(User newUser)
        {
            await UnitOfWork.BeginTransactionAsync(IsolationLevel.Serializable);
            if (!await userRepository.ExistsAsync(new UserWithEmailSpec(newUser.Email) || new UserWithNameSpec(newUser.Name)))
            {
                userRepository.Add(newUser);
                await UnitOfWork.SaveAsync();
                UnitOfWork.TransactionCommit();
                return UserModificationResult.Success;
            }

            UnitOfWork.TransactionRollback();
            return UserModificationResult.DuplicateValues;
        }

        public async Task<EmailConfirmationResult> ConfirmEmail(string confirmationCode)
        {
            User user = await userRepository.GetEntityAsync(new UserWithConfirmationCode(confirmationCode));
            if (user != null)
            {
                if (!user.IsEmailConfirmed)
                {
                    user.IsEmailConfirmed = true;
                    userRepository.Update(user);
                    await UnitOfWork.SaveAsync();
                    return EmailConfirmationResult.Success;
                }
                return EmailConfirmationResult.AlreadyConfirmed;
            }
            return EmailConfirmationResult.UserNotFound;
        }

        public async Task<bool> CheckCredentials(string email, string password)
        {
            var user = await userRepository.GetEntityAsync(new UserWithEmailSpec(email));
            if (user != null)
            {
                return user.VerifyPassword(password);
            }

            return false;
        }

    }
}
