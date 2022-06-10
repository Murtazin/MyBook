using System.ComponentModel.DataAnnotations;

namespace MyBook.WEB.Models;

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "Укажите почту")]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "Укажите пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Подтвердите пароль")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    public string Token { get; set; } = null!;
}