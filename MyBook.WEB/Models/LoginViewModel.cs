using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace MyBook.WEB.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Укажите почту")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Укажите пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    public string ReturnUrl { get; set; } = null!;

    public IList<AuthenticationScheme> ExternalLogins { get; set; } = null!;
}