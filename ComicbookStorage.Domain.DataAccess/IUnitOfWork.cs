
namespace ComicbookStorage.Domain.DataAccess
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
        Task BeginTransactionAsync();
        Task TransactionCommitAsync();
        Task TransactionRollbackAsync();
    }
}
