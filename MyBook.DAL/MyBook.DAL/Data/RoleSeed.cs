using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBook.Entity.Identity;

namespace MyBook.DataAccess.Seed;

public class RoleSeed : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = Guid.Parse("6f17d951-3ad5-49f9-b333-2a37e367333d"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                
            },
            new Role
            {
                Id = Guid.Parse("12d534a7-4535-4819-8704-bcfd7553ab46"),
                Name = "User",
                NormalizedName = "USER"
            },
            new Role
            {
                Id = Guid.Parse("6dc02633-d464-4f86-8575-4cb190d670a6"),
                Name = "UserSub",
                NormalizedName = "USERSUB"
            });
    }
}