
namespace ComicbookStorage.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Base;
    using Core.Entities;
    using DataAccess;
    using DataAccess.Repositories;

    public interface IComicbookManager : IManager
    {
        Task<(bool hasMore, IReadOnlyList<Comicbook> comicbooks)> GetPage(int pageNumber, int pageSize);
    }

    public class ComicbookManager : ManagerBase, IComicbookManager
    {
        private readonly IComicbookRepository comicbookRepository;

        public ComicbookManager(IComicbookRepository comicbookRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.comicbookRepository = comicbookRepository;
        }

        public Task<(bool hasMore, IReadOnlyList<Comicbook> comicbooks)> GetPage(int pageNumber, int pageSize)
        {
            return comicbookRepository.GetPageAsync(pageNumber, pageSize);
        }
    }
}
