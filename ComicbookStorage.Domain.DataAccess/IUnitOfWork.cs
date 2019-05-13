
namespace ComicbookStorage.Domain.DataAccess
{
    using System.Data;
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
        Task BeginTransactionAsync(IsolationLevel isolationLevel);
        void TransactionCommit();
        void TransactionRollback();
    }
}
