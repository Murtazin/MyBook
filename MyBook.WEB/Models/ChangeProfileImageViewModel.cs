using System.ComponentModel.DataAnnotations;

namespace MyBook.WEB.Models;

public class ChangeProfileImageViewModel
{
    [Required]
    public IFormFile Image { get; set; } = null!;
}