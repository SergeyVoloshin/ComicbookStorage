
namespace ComicbookStorage.Application.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Base;
    using Domain.OperationResults;
    using DTOs.Account;
    using Infrastructure.Localization;
    using Microsoft.Net.Http.Headers;
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
        public async Task<IActionResult> ConfirmEmail(string confirmationCode)
        {
            var result = await accountService.ConfirmEmail(confirmationCode);
            switch (result)
            {
                case EmailConfirmationResult.Success:
                    return Ok();
                case EmailConfirmationResult.UserNotFound:
                    return BadRequest(nameof(confirmationCode), LocalizedResources.EmailConfirmationCodeNotFound);
                case EmailConfirmationResult.AlreadyConfirmed:
                    return BadRequest(nameof(confirmationCode), LocalizedResources.UserEmailIsAlreadyConfirmed);
                default:
                    throw new InvalidOperationException(nameof(EmailConfirmationResult));
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto logInDto)
        {
            var authenticationResponse = await accountService.Authorize(logInDto, Request.Headers[HeaderNames.UserAgent].ToString());
            if (authenticationResponse == null)
            {
                return BadRequest<LogInDto>(r => r.Password, LocalizedResources.UserAuthenticationError);
            }
            return Ok(authenticationResponse);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto tokens)
        {
            var authenticationResponse = await accountService.RefreshToken(tokens, Request.Headers[HeaderNames.UserAgent].ToString());
            if (authenticationResponse == null)
            {
                return BadRequest<RefreshTokenDto>(r => r.RefreshToken, LocalizedResources.RefreshTokenError);
            }
            return Ok(authenticationResponse);
        }
    }
}
