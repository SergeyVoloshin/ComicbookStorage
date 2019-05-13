
namespace ComicbookStorage.Infrastructure.EF.Repositories
{
    using Base;
    using Domain.Core.Entities;
    using Domain.DataAccess.Repositories;

    public class ComicbookRepository : AggregateRepositoryBase<Comicbook>, IComicbookRepository
    {
        public ComicbookRepository(ComicbookStorageContext context) : base(context)
        {
        }
    }
}
