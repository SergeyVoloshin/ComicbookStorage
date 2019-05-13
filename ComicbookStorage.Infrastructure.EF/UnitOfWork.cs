
namespace ComicbookStorage.Infrastructure.EF
{
    using System;
    using Domain.DataAccess;
    using Microsoft.EntityFrameworkCore.Storage;
    using System.Threading.Tasks;
    using System.Data;
    using Microsoft.EntityFrameworkCore;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ComicbookStorageContext context;

        private IDbContextTransaction transaction;

        public UnitOfWork(ComicbookStorageContext context)
        {
            this.context = context;
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            if (transaction == null)
            {
                transaction = await context.Database.BeginTransactionAsync(isolationLevel);
            }
        }

        public void TransactionCommit()
        {
            transaction.Commit();
            transaction.Dispose();
            transaction = null;
        }

        public void TransactionRollback()
        {
            transaction.Rollback();
            transaction.Dispose();
            transaction = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                transaction?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
