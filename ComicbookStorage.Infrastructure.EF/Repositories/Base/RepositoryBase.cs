
namespace ComicbookStorage.Infrastructure.EF.Repositories.Base
{
    using Domain.Core.Entities.Base;
    using LinqSpecs;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class RepositoryBase<TEntity> where TEntity : class, IAggregateRoot
    {
        protected readonly IQueryable<TEntity> DbSet;

        protected readonly ComicbookStorageContext Context;

        protected RepositoryBase(ComicbookStorageContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>().AsNoTracking();
        }

        public Task<TEntity> GetEntityAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<TEntity> GetEntityAsync(Specification<TEntity> specification)
        {
            return DbSet.FirstOrDefaultAsync(specification);
        }

        public void Add(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }

        public void AddGraph(TEntity graph)
        {
            TrackGraph(graph, EntityState.Unchanged);
        }

        public void UpdateGraph(TEntity graph)
        {
            TrackGraph(graph, EntityState.Modified);
        }

        public Task<TEntity> GetAsync(int id)
        {
            return GetBaseQuery().FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<TEntity> GetAsync(Specification<TEntity> specification)
        {
            return GetBaseQuery().FirstOrDefaultAsync(specification);
        }

        public Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return GetBaseQuery().ToReadOnlyCollectionAsync();
        }

        public Task<IReadOnlyList<TEntity>> GetAllAsync(Specification<TEntity> specification)
        {
            return GetBaseQuery().Where(specification).ToReadOnlyCollectionAsync();
        }

        public Task<IReadOnlyList<TEntity>> GetTopAsync(Specification<TEntity> specification, int count)
        {
            return GetBaseQuery().Where(specification).Take(count).ToReadOnlyCollectionAsync();
        }

        public Task<(bool hasMore, IReadOnlyList<TEntity> entities)> GetPageAsync(Specification<TEntity> specification, int pageNumber, int pageSize)
        {
            return GetPageAsync(true, specification, pageNumber, pageSize);
        }

        public Task<(bool hasMore, IReadOnlyList<TEntity> entities)> GetPageAsync(int pageNumber, int pageSize)
        {
            return GetPageAsync(false, null, pageNumber, pageSize);
        }

        public Task<int> CountAsync(Specification<TEntity> specification)
        {
            return GetBaseQuery().CountAsync(specification);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return GetBaseQuery().Where(e => e.Id == id).ExistsAsync();
        }

        public Task<bool> ExistsAsync(Specification<TEntity> specification)
        {
            return GetBaseQuery().Where(specification).ExistsAsync();
        }

        protected virtual IQueryable<TEntity> GetBaseQuery()
        {
            return DbSet;
        }

        private async Task<(bool hasMore, IReadOnlyList<TEntity> entities)> GetPageAsync(bool applySpecification, Specification<TEntity> specification, int pageNumber, int pageSize)
        {
            IQueryable<TEntity> query = GetBaseQuery();
            if (applySpecification)
            {
                query = GetBaseQuery().Where(specification);
            }
            var entities = await query.Page(pageNumber, pageSize).ToReadOnlyCollectionAsync();
            if (entities.Count < pageSize)
            {
                return (false, entities);
            }

            return (await query.CountAsync() > pageSize * pageNumber, entities);
        }

        private void TrackGraph(TEntity graph, EntityState existingNodeState)
        {
            Context.ChangeTracker.TrackGraph(graph, e => { e.Entry.State = e.Entry.IsKeySet ? existingNodeState : EntityState.Added; });
        }
    }
}
