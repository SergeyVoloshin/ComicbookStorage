

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

        [HttpGet]
        public async Task<IEnumerable<ComicbookListDto>> GetComicbooks()
        {
            return await comicbookService.GetAllAsync();
        }
    }
}