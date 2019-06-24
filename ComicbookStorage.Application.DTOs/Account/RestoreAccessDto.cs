
namespace ComicbookStorage.Application.DTOs.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RestoreAccessDto
    {
        [Required]
        public string Email { get; set; }
    }
}
