
namespace ComicbookStorage.Application.DTOs.Account
{
    using System.ComponentModel.DataAnnotations;
    using ValidationAttributes;

    public class UpdateUserDto
    {
        [ConditionalStringLength(255, TolerateEmptyValues = true)]
        [EmailAddress]
        public string Email { get; set; }

        [ConditionalStringLength(50, MinimumLength = 5, TolerateEmptyValues = true)]
        public string Name { get; set; }

        public string OldPassword { get; set; }

        [ConditionalStringLength(255, MinimumLength = 5, TolerateEmptyValues = true)]
        public string NewPassword { get; set; }
    }
}
