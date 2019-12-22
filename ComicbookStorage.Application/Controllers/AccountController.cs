
namespace ComicbookStorage.Application.Controllers
{
    using Base;
    using Domain.OperationResults;
    using DTOs.Account;
    using Infrastructure.Localization;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Services;
    using System;
    using System.Threading.Tasks;

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
            return accountService.IsUserEmailTaken(email, AuthenticatedUserEmail);
        }

        [HttpGet("{name}")]
        public Task<bool> IsNameTaken(string name)
        {
            return accountService.IsUserNameTaken(name, AuthenticatedUserEmail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            var result = await accountService.CreateUser(user);
            switch (result)
            {
                case UserModificationResult.SuccessConfirmationRequired:
                    return Ok();
                case UserModificationResult.DuplicateValues:
                    return BadRequest<CreateUserDto>(u => u.Email, LocalizedResources.UserDuplicateValuesError);
                default:
                    throw new InvalidOperationException(nameof(UserModificationResult));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserDto user)
        {
            var result = await accountService.UpdateUser(AuthenticatedUserEmail, user);
            switch (result)
            {
                case UserModificationResult.NothingToUpdate:
                case UserModificationResult.SuccessNoConfirmationRequired:
                    return Ok(new UpdateUserResultDto { IsConfirmationRequired = false });
                case UserModificationResult.SuccessConfirmationRequired:
                    return Ok(new UpdateUserResultDto { IsConfirmationRequired = true });
                case UserModificationResult.DuplicateValues:
                    return BadRequest<UpdateUserDto>(u => u.Email, LocalizedResources.UserDuplicateValuesError);
                case UserModificationResult.IncorrectPassword:
                    return BadRequest<UpdateUserDto>(u => u.OldPassword, LocalizedResources.UserIncorrectPasswordError);
                default:
                    throw new InvalidOperationException(nameof(UserModificationResult));
            }
        }

        [HttpGet]
        [Authorize]
        public Task<UserDataDto> GetUserData()
        {
            return accountService.GetUserData(AuthenticatedUserEmail);
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

        [HttpPost]
        public async Task<IActionResult> Restore(RestoreAccessDto restoreRequest)
        {
            var result = await accountService.RestoreAccess(restoreRequest);
            switch (result)
            {
                case RestoreAccessResult.ResendConfirmationCode:
                case RestoreAccessResult.ResetPassword:
                    return Ok();
                case RestoreAccessResult.NotFound:
                    return BadRequest<RestoreAccessDto>(r => r.Email, LocalizedResources.EmailNotFoundError);
                default:
                    throw new InvalidOperationException(nameof(RestoreAccessResult));
            }
        }
    }
}
