using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess.Seed;

public partial class Seeds
{
    public static void CreateUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasData(new
            {
                //passowrd: qwe123QWE_
                Id = Guid.Parse("4bee3a36-db98-4071-ad61-a61db810decb"),
                UserName = "admin@mybook.ru",
                NormalizedUserName = "ADMIN@MYBOOK.RU",
                Email = "admin@mybook.ru",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "qwe123QWE_"),
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Image = Convert.ToBase64String(File.ReadAllBytes("wwwroot/img/user.png")),
                LastName = "Admin",
                Name = "Admin",
                SubId = 4,
                SubDateStart = default(DateTime)
            });
        });
    }
}