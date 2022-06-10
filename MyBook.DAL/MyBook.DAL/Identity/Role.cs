using Microsoft.AspNetCore.Identity;

namespace MyBook.Entity.Identity;

public class Role : IdentityRole<Guid>
{
    public virtual ICollection<UserRole> UserRoles { get; set; }
}