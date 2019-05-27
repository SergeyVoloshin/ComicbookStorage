
namespace ComicbookStorage.Application.Services
{
    using AutoMapper;
    using Base;
    using ComicbookStorage.Domain.Services;
    using DTOs.Comicbook;
    using System.Linq;
    using System.Threading.Tasks;
    using Configuration;
    using Flurl;

    public interface IComicbookService : IService
    {
        Task<ComicbookListPageDto> GetPage(int pageNumber, int pageSize);
    }

    public class ComicbookService : ServiceBase, IComicbookService
    {
        private readonly IAppConfiguration appConfig;
        private readonly IComicbookManager comicbookManager;

        public ComicbookService(IAppConfiguration appConfig, IComicbookManager comicbookManager, IMapper mapper) : base(mapper)
        {
            this.appConfig = appConfig;
            this.comicbookManager = comicbookManager;
        }

        public async Task<ComicbookListPageDto> GetPage(int pageNumber, int pageSize)
        {
            var (hasMore, comicbooks) = await comicbookManager.GetPage(pageNumber, pageSize);
            var mappedComicbooks = comicbooks.Select(c => 
                Mapper.Map(c, new ComicbookListItemDto(Url.Combine(appConfig.ComicbookImagePath, c.UserFriendlyId, $"{appConfig.SmallCoverName}.{c.CoverExtension}"))));
            return new ComicbookListPageDto(hasMore, mappedComicbooks);
        }
    }
}
