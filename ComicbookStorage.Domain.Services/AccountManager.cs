
namespace ComicbookStorage.Domain.Services
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Base;
    using Core.Entities.Specifications.User;
    using DataAccess;
    using DataAccess.Repositories;

    public interface IAccountManager : IManager
    {
        Task<bool> IsUserEmailTaken(string email);

        Task<bool> IsUserNameTaken(string name);
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
    }
}
