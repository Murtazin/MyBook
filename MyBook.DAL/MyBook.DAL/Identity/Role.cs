using Microsoft.AspNetCore.Identity;

namespace MyBook.DAL.Identity;

public class Role : IdentityRole<Guid>
{
    public virtual ICollection<UserRole> UserRoles { get; set; }
}