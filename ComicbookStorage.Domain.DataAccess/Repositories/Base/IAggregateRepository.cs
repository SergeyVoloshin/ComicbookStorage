
namespace ComicbookStorage.Domain.DataAccess.Repositories.Base
{
    using System.Threading.Tasks;
    using Core.Entities.Base;
    using LinqSpecs;

    public interface IAggregateRepository<TEntity> : IEntityRepository<TEntity> where TEntity : IAggregateRoot
    {
        Task<TEntity> GetAggregateAsync(Specification<TEntity> specification);
        Task<TEntity> GetAggregateAsync(int id);
    }
}
