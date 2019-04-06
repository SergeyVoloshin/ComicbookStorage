
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
        Task<IEnumerable<Comicbook>> GetAllAsync();
    }

    public class ComicbookManager : ManagerBase, IComicbookManager
    {
        private readonly IComicbookRepository comicbookRepository;

        public ComicbookManager(IComicbookRepository comicbookRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.comicbookRepository = comicbookRepository;
        }

        public Task<IEnumerable<Comicbook>> GetAllAsync()
        {
            return comicbookRepository.GetAllAsync();
        }
    }
}
