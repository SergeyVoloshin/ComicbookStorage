
namespace ComicbookStorage.Infrastructure.EF.Repositories.Base
{
    using Domain.Core.Entities.Base;
    using LinqSpecs;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class AggregateRepositoryBase<TEntity> : EntityRepositoryBase<TEntity> where TEntity : class, IAggregateRoot
    {
        protected AggregateRepositoryBase(ComicbookStorageContext context) : base(context)
        {
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
    }
}
