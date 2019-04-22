
namespace ComicbookStorage.Domain.Services
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Base;
    using Core.Entities;
    using DataAccess;
    using DataAccess.Repositories;

    public interface IComicbookManager : IManager
    {
        Task<(bool hasMore, ReadOnlyCollection<Comicbook> comicbooks)> GetPage(uint pageNumber, uint pageSize);
    }

    public class ComicbookManager : ManagerBase, IComicbookManager
    {
        private readonly IComicbookRepository comicbookRepository;

        public ComicbookManager(IComicbookRepository comicbookRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.comicbookRepository = comicbookRepository;
        }

        public Task<(bool hasMore, ReadOnlyCollection<Comicbook> comicbooks)> GetPage(uint pageNumber, uint pageSize)
        {
            return comicbookRepository.GetPageAsync(pageNumber, pageSize);
        }
    }
}
