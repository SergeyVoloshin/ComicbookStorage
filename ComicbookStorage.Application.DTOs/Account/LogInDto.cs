
namespace ComicbookStorage.Application.DTOs.Account
{
    using System.ComponentModel.DataAnnotations;

    public class LogInDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
