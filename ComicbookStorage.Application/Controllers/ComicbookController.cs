

namespace ComicbookStorage.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTOs.Comicbook;
    using Application.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ComicbookController : Controller
    {
        private readonly IComicbookService comicbookService;

        public ComicbookController(IComicbookService comicbookService)
        {
            this.comicbookService = comicbookService;
        }

        [HttpGet("{pageNumber}/{pageSize}/")]
        public async Task<ComicbookListPageDto> GetComicbooks(uint pageNumber, uint pageSize)
        {

            return await comicbookService.GetPage(pageNumber, pageSize);
        }
    }
}