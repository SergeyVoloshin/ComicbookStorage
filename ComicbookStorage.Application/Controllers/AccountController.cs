
namespace ComicbookStorage.Application.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Base;
    using DTOs.Account;
    using Services;

    public class AccountController : ApplicationControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("{email}")]
        public Task<bool> IsEmailTaken(string email)
        {
            return accountService.IsUserEmailTaken(email);
        }

        [HttpGet("{name}")]
        public Task<bool> IsNameTaken(string name)
        {
            return accountService.IsUserNameTaken(name);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            await accountService.IsUserNameTaken(user.Name);
            return Ok();
        }
    }
}
