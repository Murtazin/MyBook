using Microsoft.AspNetCore.Identity;
using MyBook.Entity.Identity;

namespace MyBook.Entity;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Subscription Sub { get; set; } = null!;
    public int SubId { get; set; }
    
    public DateTime SubDateStart { get; set; }
    public string Image { get; set; } = null!;
    public List<Book> FavoriteBooks { get; set; } = null!;
    
    public virtual ICollection<UserRole> UserRoles { get; set; }
}