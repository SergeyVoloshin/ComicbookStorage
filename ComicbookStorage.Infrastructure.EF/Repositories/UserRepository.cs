
namespace ComicbookStorage.Infrastructure.EF.Repositories
{
    using Base;
    using Domain.Core.Entities;
    using Domain.DataAccess.Repositories;

    public class UserRepository : AggregateRepositoryBase<User>, IUserRepository
    {
        public UserRepository(ComicbookStorageContext context) : base(context)
        {
        }
    }
}
