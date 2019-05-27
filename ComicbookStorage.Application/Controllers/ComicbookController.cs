
namespace ComicbookStorage.Controllers
{
    using System.Threading.Tasks;
    using Application.Controllers.Base;
    using Application.DTOs.Comicbook;
    using Application.Services;
    using Microsoft.AspNetCore.Mvc;

    public class ComicbookController : ApplicationControllerBase
    {
        private readonly IComicbookService comicbookService;

        public ComicbookController(IComicbookService comicbookService)
        {
            this.comicbookService = comicbookService;
        }

        [HttpGet("{pageNumber}/{pageSize}/")]
        public async Task<ComicbookListPageDto> GetPage(int pageNumber, int pageSize)
        {
            return await comicbookService.GetPage(pageNumber, pageSize);
        }
    }
}