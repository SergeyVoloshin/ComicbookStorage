
namespace ComicbookStorage.Domain.DataAccess.Repositories.Base
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Entities.Base;
    using LinqSpecs;

    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync(Specification<T> specification);
        Task<T> GetAsync(Specification<T> specification);
        Task<int> GetIdAsync(Specification<T> specification);
        Task<int> CountAsync(Specification<T> specification);
        Task<bool> ExistsAsync(Specification<T> specification);
        Task<bool> ExistsAsync(int id);
        Task<T> GetAggregateAsync(Specification<T> specification);
        Task<T> GetAggregateAsync(int id);
        Task<IEnumerable<T>> GetAggregatesAsync(Specification<T> specification);
    }
}
