
namespace ComicbookStorage.Infrastructure.EF
{
    using System.Threading.Tasks;
    using Domain.DataAccess;
    using Microsoft.EntityFrameworkCore.Storage;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ComicbookStorageContext context;

        private Task<IDbContextTransaction> transactionTask;

        public UnitOfWork(ComicbookStorageContext context)
        {
            this.context = context;
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public Task BeginTransactionAsync()
        {
            if (transactionTask == null)
            {
                transactionTask = context.Database.BeginTransactionAsync();
            }
            return transactionTask;
        }

        public async Task TransactionCommitAsync()
        {
            using (var transaction = await transactionTask)
            {
                transaction.Commit();
            }
            transactionTask = null;
        }

        public async Task TransactionRollbackAsync()
        {
            using (var transaction = await transactionTask)
            {
                transaction.Rollback();
            }
            transactionTask = null;
        }
    }
}
