
namespace ComicbookStorage.Application.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Threading.Tasks;
    using Base;
    using Domain.OperationResults;
    using DTOs.Account;
    using Infrastructure.Localization;
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
            var result = await accountService.CreateUser(user);
            switch (result)
            {
                case UserModificationResult.Success:
                    return Ok();
                case UserModificationResult.DuplicateValues:
                    return BadRequest<CreateUserDto>(u => u.Email, LocalizedResources.UserDuplicateValuesError);
                default:
                    throw new InvalidOperationException(nameof(UserModificationResult));
            }
        }

        [HttpPut("{confirmationCode}")]
        public IActionResult ConfirmEmail(string confirmationCode)
        {
            return Ok();
        }
    }
}
