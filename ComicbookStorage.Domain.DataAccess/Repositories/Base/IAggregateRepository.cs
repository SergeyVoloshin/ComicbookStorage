
namespace ComicbookStorage.Domain.DataAccess.Repositories.Base
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Core.Entities.Base;
    using LinqSpecs;

    public interface IAggregateRepository<TEntity> : IEntityRepository<TEntity> where TEntity : IAggregateRoot
    {
        Task<ReadOnlyCollection<TEntity>> GetAllAsync();
        Task<ReadOnlyCollection<TEntity>> GetAllAsync(Specification<TEntity> specification);
        Task<(bool hasMore, ReadOnlyCollection<TEntity> entities)> GetPageAsync(Specification<TEntity> specification, uint pageNumber, uint pageSize);
        Task<(bool hasMore, ReadOnlyCollection<TEntity> entities)> GetPageAsync(uint pageNumber, uint pageSize);
        Task<TEntity> GetAsync(Specification<TEntity> specification);
        Task<int> CountAsync(Specification<TEntity> specification);
        Task<bool> ExistsAsync(Specification<TEntity> specification);
        Task<bool> ExistsAsync(int id);
        Task<TEntity> GetAggregateAsync(Specification<TEntity> specification);
        Task<TEntity> GetAggregateAsync(int id);
    }
}
