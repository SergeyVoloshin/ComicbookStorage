
namespace ComicbookStorage.Application.Services
{
    using AutoMapper;
    using Base;
    using ComicbookStorage.Domain.Services;
    using DTOs.Comicbook;
    using System.Linq;
    using System.Threading.Tasks;
    using Configuration;

    public interface IComicbookService : IService
    {
        Task<ComicbookListPageDto> GetPage(uint pageNumber, uint pageSize);
    }

    public class ComicbookService : ServiceBase, IComicbookService
    {
        private readonly IPathConfiguration pathConfig;
        private readonly IComicbookManager comicbookManager;

        public ComicbookService(IPathConfiguration pathConfig, IComicbookManager comicbookManager, IMapper mapper) : base(mapper)
        {
            this.pathConfig = pathConfig;
            this.comicbookManager = comicbookManager;
        }

        public async Task<ComicbookListPageDto> GetPage(uint pageNumber, uint pageSize)
        {
            var (hasMore, comicbooks) = await comicbookManager.GetPage(pageNumber, pageSize);
            var mappedComicbooks = comicbooks.Select(c => Mapper.Map(c, new ComicbookListItemDto($"{pathConfig.ComicbookImages}{c.SeoUrl}/cover.jpg")));
            return new ComicbookListPageDto(hasMore, mappedComicbooks);
        }
    }
}
