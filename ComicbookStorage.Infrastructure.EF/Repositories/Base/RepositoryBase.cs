﻿
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
        protected readonly IQueryable<TEntity> DbSet;

        protected readonly ComicbookStorageContext Context;

        protected RepositoryBase(ComicbookStorageContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>().AsNoTracking();
        }

        public Task<TEntity> GetAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(e => e.Id == id);
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

        public void AddGraph(TEntity entity)
        {
            TrackGraph(entity, EntityState.Unchanged);
        }

        public void UpdateGraph(TEntity entity)
        {
            TrackGraph(entity, EntityState.Modified);
        }

        public Task<TEntity> GetAggregateAsync(Specification<TEntity> specification)
        {
            return GetBaseQuery().FirstOrDefaultAsync(specification);
        }

        public Task<TEntity> GetAggregateAsync(int id)
        {
            return GetBaseQuery().FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<ReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return DbSet.ToReadOnlyCollectionAsync();
        }

        public Task<ReadOnlyCollection<TEntity>> GetAllAsync(Specification<TEntity> specification)
        {
            return DbSet.Where(specification).ToReadOnlyCollectionAsync();
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
            return DbSet.FirstOrDefaultAsync(specification);
        }

        public Task<int> CountAsync(Specification<TEntity> specification)
        {
            return DbSet.CountAsync(specification);
        }

        public Task<bool> ExistsAsync(Specification<TEntity> specification)
        {
            return DbSet.Where(specification).ExistsAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return DbSet.Where(e => e.Id == id).ExistsAsync();
        }

        protected virtual IQueryable<TEntity> GetBaseQuery()
        {
            throw new NotImplementedException();
        }

        private async Task<(bool hasMore, ReadOnlyCollection<TEntity> entities)> GetPageAsync(bool applySpecification, Specification<TEntity> specification, uint pageNumber, uint pageSize)
        {
            IQueryable<TEntity> query = applySpecification ? DbSet.Where(specification) : DbSet;
            var entities = await query.Page(pageNumber, pageSize).ToReadOnlyCollectionAsync();
            if (entities.Count < pageSize)
            {
                return (false, entities);
            }

            return (await query.CountAsync() > pageSize * pageNumber, entities);
        }

        private void TrackGraph(TEntity entity, EntityState existingNodeState)
        {
            Context.ChangeTracker.TrackGraph(entity, e => { e.Entry.State = e.Entry.IsKeySet ? existingNodeState : EntityState.Added; });
        }
    }
}