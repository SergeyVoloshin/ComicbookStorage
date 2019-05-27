
namespace ComicbookStorage.Infrastructure.EF.Repositories.Base
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Tasks;

    internal static class RepositoryExtensions
    {
        public static IQueryable<TEntity> Page<TEntity>(this IQueryable<TEntity> source, int pageNumber, int pageSize)
        {
            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static Task<IEnumerable<TEntity>> ToEnumerableAsync<TEntity>(this IQueryable<TEntity> source)
        {
            return source.ToListAsync().Transform(r => (IEnumerable<TEntity>)r);
        }

        public static Task<IReadOnlyList<TEntity>> ToReadOnlyCollectionAsync<TEntity>(this IQueryable<TEntity> source)
        {
            return source.ToListAsync().Transform(r => (IReadOnlyList<TEntity>)r.AsReadOnly());
        }

        public static Task<bool> ExistsAsync<TEntity>(this IQueryable<TEntity> source)
        {
            return source.CountAsync().Transform(r => r > 0);
        }
    }
}
