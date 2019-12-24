
namespace ComicbookStorage.Domain.Services
{
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using Base;
    using Core.Entities;
    using Core.Entities.Specifications.User;
    using DataAccess;
    using DataAccess.Repositories;
    using LinqSpecs;
    using OperationResults;

    public interface IAccountManager : IManager
    {
        Task<bool> IsUserEmailTaken(string email, string userEmail);

        Task<bool> IsUserNameTaken(string name, string userEmail);

        Task<UserModificationResult> CreateUser(User newUser);

        Task<(UserModificationResult result, User user)> UpdateUser(string email, string newEmail, string newName, string newPassword, string oldPassword);

        Task<EmailConfirmationResult> ConfirmEmail(string confirmationCode);

        Task<(RestoreAccessResult result, User user, string newPassword)> RestoreAccess(string email);

        Task<string> Authorize(string email, string password, string userAgent, double tokenLifeTimeMinutes);

        Task<string> RefreshToken(string email, string userRefreshToken, string userAgent, double tokenLifeTimeMinutes);

        Task<User> GetUserData(string userEmail);
    }

    public class AccountManager : ManagerBase, IAccountManager
    {
        private readonly IUserRepository userRepository;

        public AccountManager(IUserRepository userRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.userRepository = userRepository;
        }

        public Task<bool> IsUserEmailTaken(string email, string userEmail)
        {
            var spec = NotAuthenticatedUser(userEmail, new UserWithEmailSpec(email));
            return userRepository.ExistsAsync(spec);
        }

        public Task<bool> IsUserNameTaken(string name, string userEmail)
        {
            var spec = NotAuthenticatedUser(userEmail, new UserWithNameSpec(name));
            return userRepository.ExistsAsync(spec);
        }

        public async Task<UserModificationResult> CreateUser(User newUser)
        {
            await UnitOfWork.BeginTransactionAsync(IsolationLevel.Serializable);
            if (!await userRepository.ExistsAsync(new UserWithEmailSpec(newUser.Email) || new UserWithNameSpec(newUser.Name)))
            {
                userRepository.Add(newUser);
                await UnitOfWork.SaveAsync();
                UnitOfWork.TransactionCommit();
                return UserModificationResult.SuccessConfirmationRequired;
            }

            UnitOfWork.TransactionRollback();
            return UserModificationResult.DuplicateValues;
        }

        public async Task<(UserModificationResult result, User user)> UpdateUser(string email, string newEmail, string newName, string newPassword, string oldPassword)
        {
            var user = await userRepository.GetEntityAsync(new UserWithEmailSpec(email));

            UserModificationResult result = UserModificationResult.NothingToUpdate;

            if (!string.IsNullOrEmpty(newName))
            {
                var userWithNameSpec = new UserWithNameSpec(newName);
                var isEqualToCurrentName = userWithNameSpec.ToExpression().Compile();
                if (!isEqualToCurrentName(user))
                {
                    if (await userRepository.ExistsAsync(userWithNameSpec))
                    {
                        return (UserModificationResult.DuplicateValues, null);
                    }
                    user.Name = newName;
                    result = UserModificationResult.SuccessNoConfirmationRequired;
                }
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                if (!user.VerifyPassword(oldPassword))
                {
                    return (UserModificationResult.IncorrectPassword, null);
                }
                user.SetPassword(newPassword);
                result = UserModificationResult.SuccessNoConfirmationRequired;
            }

            if (!string.IsNullOrEmpty(newEmail))
            {
                var userWithEmailSpec = new UserWithEmailSpec(newEmail);
                var isEqualToCurrentEmail = userWithEmailSpec.ToExpression().Compile();
                if (!isEqualToCurrentEmail(user))
                {
                    if (await userRepository.ExistsAsync(userWithEmailSpec))
                    {
                        return (UserModificationResult.DuplicateValues, null);
                    }
                    user.SetEmail(email);
                    result = UserModificationResult.SuccessConfirmationRequired;
                }
            }

            if (result != UserModificationResult.NothingToUpdate)
            {
                userRepository.Update(user);
                await UnitOfWork.SaveAsync();
            }

            return (result, user);
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

        public async Task<(RestoreAccessResult result, User user, string newPassword)> RestoreAccess(string email)
        {
            User user = await userRepository.GetEntityAsync(new UserWithEmailSpec(email));
            if (user != null)
            {
                if (!user.IsEmailConfirmed)
                {
                    return (RestoreAccessResult.ResendConfirmationCode, user, null);
                }

                string newPassword = user.ResetPassword();
                userRepository.Update(user);
                await UnitOfWork.SaveAsync();
                return (RestoreAccessResult.ResetPassword, user, newPassword);
            }
            return (RestoreAccessResult.NotFound, null, null);
        }

        public Task<string> Authorize(string email, string password, string userAgent, double tokenLifeTimeMinutes)
        {
            return Authorize(email, userAgent, tokenLifeTimeMinutes, user => user.VerifyPassword(password));
        }

        public Task<string> RefreshToken(string email, string userRefreshToken, string userAgent, double tokenLifeTimeMinutes)
        {
            return Authorize(email, userAgent, tokenLifeTimeMinutes, user => user.VerifyRefreshToken(userAgent, userRefreshToken));
        }

        public Task<User> GetUserData(string userEmail)
        {
            return userRepository.GetAsync(new UserWithEmailSpec(userEmail));
        }

        private async Task<string> Authorize(string email, string userAgent, double tokenLifeTimeMinutes, Func<User, bool> verify)
        {
            var user = await userRepository.GetEntityAsync(new UserWithEmailSpec(email));
            if (user != null && verify(user))
            {
                user.GenerateRefreshToken(userAgent, tokenLifeTimeMinutes);
                userRepository.Update(user);
                await UnitOfWork.SaveAsync();
                return user.RefreshToken;
            }

            return null;
        }

        private static Specification<User> NotAuthenticatedUser(string userEmail, Specification<User> spec)
        {
            if (userEmail != null)
            {
                return spec && !new UserWithEmailSpec(userEmail);
            }
            return spec;
        }
    }
}
