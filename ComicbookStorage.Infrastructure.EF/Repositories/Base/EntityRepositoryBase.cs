

namespace ComicbookStorage.Infrastructure.EF.Repositories.Base
{
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Core.Entities.Base;
    using Microsoft.EntityFrameworkCore;

    public class EntityRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        private readonly IQueryable<TEntity> dbSet;

        private readonly ComicbookStorageContext context;

        protected EntityRepositoryBase(ComicbookStorageContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>().AsNoTracking();
        }

        public Task<TEntity> GetAsync(int id)
        {
            return dbSet.FirstOrDefaultAsync(e => e.Id == id);
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
    }
}
