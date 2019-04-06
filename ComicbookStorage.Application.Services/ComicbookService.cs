
namespace ComicbookStorage.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Base;
    using ComicbookStorage.Domain.Services;
    using Domain.Core.Entities;
    using Infrastructure.Tasks;
    using DTOs.Comicbook;

    public interface IComicbookService : IService
    {
        Task<IEnumerable<ComicbookListDto>> GetAllAsync();
    }

    public class ComicbookService : ServiceBase, IComicbookService
    {
        private readonly IComicbookManager comicbookManager;

        public ComicbookService(IComicbookManager comicbookManager, IMapper mapper) : base(mapper)
        {
            this.comicbookManager = comicbookManager;
        }

        public Task<IEnumerable<ComicbookListDto>> GetAllAsync()
        {
            return comicbookManager.GetAllAsync().Transform(r => Mapper.Map<IEnumerable<Comicbook>, IEnumerable<ComicbookListDto>>(r));
        }
    }
}
