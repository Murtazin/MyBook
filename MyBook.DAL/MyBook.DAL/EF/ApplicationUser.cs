using Microsoft.AspNetCore.Identity;

namespace MyBook.DAL.EF;

public class ApplicationUser : IdentityUser
{
    public string Avatar { get; set; }
}