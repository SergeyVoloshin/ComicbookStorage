
namespace ComicbookStorage.Infrastructure.EF.Repositories.Base
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Tasks;

    internal static class RepositoryExtensions
    {
        public static IQueryable<TEntity> Page<TEntity>(this IQueryable<TEntity> source, uint pageNumber, uint pageSize)
        {
            return source.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize);
        }

        public static Task<IEnumerable<TEntity>> ToEnumerableAsync<TEntity>(this IQueryable<TEntity> source)
        {
            return source.ToListAsync().Transform(r => (IEnumerable<TEntity>)r);
        }

        public static Task<ReadOnlyCollection<TEntity>> ToReadOnlyCollectionAsync<TEntity>(this IQueryable<TEntity> source)
        {
            return source.ToListAsync().Transform(r => r.AsReadOnly());
        }

        public static Task<bool> ExistsAsync<TEntity>(this IQueryable<TEntity> source)
        {
            return source.CountAsync().Transform(r => r > 0);
        }
    }
}
