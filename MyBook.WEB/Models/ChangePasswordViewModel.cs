using System.ComponentModel.DataAnnotations;

namespace MyBook.WEB.Models;

public class ChangePasswordViewModel
{
    [Required]
    public string NewPassword { get; set; } = null!;
    [Required]
    public string OldPassword { get; set; } = null!;
}