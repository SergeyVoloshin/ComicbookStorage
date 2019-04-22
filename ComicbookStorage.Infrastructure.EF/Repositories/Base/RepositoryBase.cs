
namespace ComicbookStorage.Infrastructure.EF.Repositories.Base
{
    using Domain.Core.Entities.Base;
    using LinqSpecs;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class RepositoryBase<TEntity> where TEntity : class, IAggregateRoot
    {
        private readonly IQueryable<TEntity> dbset;

        private readonly ComicbookStorageContext context;

        protected RepositoryBase(ComicbookStorageContext context)
        {
            this.context = context;
            dbset = context.Set<TEntity>().AsNoTracking();
        }

        public Task<ReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return dbset.ToReadOnlyCollectionAsync();
        }

        public Task<TEntity> GetAsync(int id)
        {
            return dbset.FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Add(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public Task<ReadOnlyCollection<TEntity>> GetAllAsync(Specification<TEntity> specification)
        {
            return dbset.Where(specification).ToReadOnlyCollectionAsync();
        }

        public Task<(bool hasMore, ReadOnlyCollection<TEntity> entities)> GetPageAsync(Specification<TEntity> specification, uint pageNumber, uint pageSize)
        {
            return GetPageAsync(true, specification, pageNumber, pageSize);
        }

        public Task<(bool hasMore, ReadOnlyCollection<TEntity> entities)> GetPageAsync(uint pageNumber, uint pageSize)
        {
            return GetPageAsync(false, null, pageNumber, pageSize);
        }

        public Task<TEntity> GetAsync(Specification<TEntity> specification)
        {
            return dbset.FirstOrDefaultAsync(specification);
        }

        public Task<int> GetIdAsync(Specification<TEntity> specification)
        {
            return dbset.Where(specification).Select(e => e.Id).FirstOrDefaultAsync();
        }

        public Task<int> CountAsync(Specification<TEntity> specification)
        {
            return dbset.CountAsync(specification);
        }

        public Task<bool> ExistsAsync(Specification<TEntity> specification)
        {
            return dbset.Where(specification).ExistsAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return dbset.Where(e => e.Id == id).ExistsAsync();
        }

        public Task<TEntity> GetAggregateAsync(Specification<TEntity> specification)
        {
            return GetBaseQuery().FirstOrDefaultAsync(specification);
        }

        public Task<TEntity> GetAggregateAsync(int id)
        {
            return GetBaseQuery().FirstOrDefaultAsync(e => e.Id == id);
        }

        protected virtual IQueryable<TEntity> GetBaseQuery()
        {
            throw new NotImplementedException();
        }

        private async Task<(bool hasMore, ReadOnlyCollection<TEntity> entities)> GetPageAsync(bool applySpecification, Specification<TEntity> specification, uint pageNumber, uint pageSize)
        {
            IQueryable<TEntity> query = applySpecification  ? dbset.Where(specification) : dbset;
            var entities = await query.Page(pageNumber, pageSize).ToReadOnlyCollectionAsync();
            if (entities.Count < pageSize)
            {
                return (false, entities);
            }

            return (await query.CountAsync() > pageSize * pageNumber, entities);
        }
    }
}
