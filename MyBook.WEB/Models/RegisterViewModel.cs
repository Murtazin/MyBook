using System.ComponentModel.DataAnnotations;

namespace MyBook.WEB.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Укажите имя")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Укажите фамилию")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Укажите пароль")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Укажите почту")]
    [EmailAddress]
    public string Email { get; set; } = null!;
}