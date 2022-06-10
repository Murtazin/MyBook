using System.ComponentModel.DataAnnotations;
using MyBook.DAL.Entities;

namespace MyBook.WEB.Models;

public class EditProfileViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;

    public string Image { get; set; } = null!;
    
    public Subscription Sub { get; set; } = null!;
    
    public double SubDurationLeft { get; set; }
}