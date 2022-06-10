using Microsoft.AspNetCore.Identity;
using MyBook.DAL.Entities;

namespace MyBook.DAL.Identity;

public class UserRole : IdentityUserRole<Guid>
{
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}