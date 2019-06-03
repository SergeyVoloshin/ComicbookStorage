
namespace ComicbookStorage.Domain.DataAccess.Repositories.Base
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Entities.Base;
    using LinqSpecs;

    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        Task<TEntity> GetEntityAsync(int id);
        Task<TEntity> GetEntityAsync(Specification<TEntity> specification);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void AddGraph(TEntity graph);
        void UpdateGraph(TEntity graph);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(Specification<TEntity> specification);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllAsync(Specification<TEntity> specification);
        Task<IReadOnlyList<TEntity>> GetTopAsync(Specification<TEntity> specification, int count);
        Task<(bool hasMore, IReadOnlyList<TEntity> entities)> GetPageAsync(Specification<TEntity> specification, int pageNumber, int pageSize);
        Task<(bool hasMore, IReadOnlyList<TEntity> entities)> GetPageAsync(int pageNumber, int pageSize);
        Task<int> CountAsync(Specification<TEntity> specification);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Specification<TEntity> specification);
    }
}
