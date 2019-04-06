
namespace ComicbookStorage.Infrastructure.EF.Repositories.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Core.Entities.Base;
    using LinqSpecs;
    using Microsoft.EntityFrameworkCore;
    using Tasks;

    public abstract class RepositoryBase<T> where T : class, IAggregateRoot
    {
        private readonly DbSet<T> dbset;

        private readonly ComicbookStorageContext context;

        protected RepositoryBase(ComicbookStorageContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return dbset.ToListAsync().Transform(r => (IEnumerable<T>)r);
        }

        public virtual Task<T> GetAsync(int id)
        {
            return dbset.FindAsync(id);
        }

        public virtual void Add(T entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public virtual void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public virtual Task<IEnumerable<T>> GetAllAsync(Specification<T> specification)
        {
            return dbset.Where(specification).ToListAsync().Transform(r => (IEnumerable<T>)r);
        }

        public virtual Task<T> GetAsync(Specification<T> specification)
        {
            return dbset.Where(specification).FirstOrDefaultAsync();
        }

        public virtual Task<int> GetIdAsync(Specification<T> specification)
        {
            return dbset.Where(specification).Select(e => e.Id).FirstOrDefaultAsync();
        }

        public virtual Task<int> CountAsync(Specification<T> specification)
        {
            return dbset.CountAsync(specification);
        }
        public virtual Task<bool> ExistsAsync(Specification<T> specification)
        {
            return CountAsync(specification).Transform(r => r > 0);
        }

        public virtual Task<bool> ExistsAsync(int id)
        {
            return dbset.CountAsync(e => e.Id == id).Transform(r => r > 0);
        }

        public virtual Task<T> GetAggregateAsync(Specification<T> specification)
        {
            return GetBaseQuery().FirstOrDefaultAsync(specification);
        }

        public virtual Task<T> GetAggregateAsync(int id)
        {
            return GetBaseQuery().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual Task<IEnumerable<T>> GetAggregatesAsync(Specification<T> specification)
        {
            return GetBaseQuery().Where(specification).ToListAsync().Transform(r => (IEnumerable<T>)r); ;
        }

        protected virtual IQueryable<T> GetBaseQuery()
        {
            throw new NotImplementedException();
        }
    }
}
