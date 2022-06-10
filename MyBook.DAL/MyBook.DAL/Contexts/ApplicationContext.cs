using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBook.DAL.Data;
using MyBook.DAL.Entities;
using MyBook.DAL.Identity;

namespace MyBook.DAL.Contexts;

// dotnet ef migrations add [название коммита] 
// dotnet ef database update 

public class ApplicationContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,
    UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>

{
    public ApplicationContext(DbContextOptions options)
        : base(options)
    {
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        // AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Subscription> Subs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Seeds.CreateSubs(modelBuilder);
        Seeds.CreateUsers(modelBuilder);
        Seeds.CreateAuthors(modelBuilder);
        Seeds.CreateBooks(modelBuilder);
        Seeds.CreateBookUser(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleSeed());
        modelBuilder.ApplyConfiguration(new UserRoleSeed());
        
        
        modelBuilder.Entity<UserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });
    }
}