
namespace ComicbookStorage.Application.Controllers.Base
{
    using System;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    [ApiController]
    [Route("[controller]/[action]")]
    public class ApplicationControllerBase : ControllerBase
    {
        protected string AuthenticatedUserEmail => User.FindFirstValue(ClaimTypes.Email);

        protected BadRequestObjectResult BadRequest<TModel>(Expression<Func<TModel, object>> expression, string errorMessage)
        {
            var errors = new ModelStateDictionary();
            errors.AddModelError(expression, errorMessage);
            return BadRequest(new ValidationProblemDetails(errors));
        }

        protected BadRequestObjectResult BadRequest(string key, string errorMessage)
        {
            var errors = new ModelStateDictionary();
            errors.AddModelError(key, errorMessage);
            return BadRequest(new ValidationProblemDetails(errors));
        }
    }
}
