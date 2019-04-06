
namespace ComicbookStorage.Domain.Services.Base
{
    using DataAccess;

    public abstract class ManagerBase
    {
        protected ManagerBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }
    }
}
