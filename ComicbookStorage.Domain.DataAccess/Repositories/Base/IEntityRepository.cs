
namespace ComicbookStorage.Domain.DataAccess.Repositories.Base
{
    using System.Threading.Tasks;
    using Core.Entities.Base;

    public interface IEntityRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> GetAsync(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
