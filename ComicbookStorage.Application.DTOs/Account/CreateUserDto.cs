
namespace ComicbookStorage.Application.DTOs.Account
{
    using System.ComponentModel.DataAnnotations;

    public class CreateUserDto
    {
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
