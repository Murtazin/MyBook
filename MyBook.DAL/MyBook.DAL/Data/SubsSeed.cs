using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess.Seed;

public partial class Seeds
{
    public static void CreateSubs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subscription>().HasData(
            new
            {
                Id = 1,
                Name = "Месяц",
                Duration = 30,
                Description =
                    "📚  Все книги\n💌  Персональные рекомендации\n👌  Первоклассная поддержка",
                Price = 349m
            },
            new
            {
                Id = 2,
                Name = "Год",
                Duration = 365,
                Description =
                    "📚  Все книги\n💌  Персональные рекомендации\n👌  Первоклассная поддержка",
                Price = 249m * 12
            },
            new
            {
                Id = 3,
                Name = "Полгода",
                Duration = 180,
                Description =
                    "📚  Все книги\n💌  Персональные рекомендации\n👌  Первоклассная поддержка",
                Price = 299m * 6
            },
            new
            {
                Id = 4,
                Name = "Бесплатно",
                Duration = -1,
                Description = "📚  Все бесплатные книги",
                Price = 0m * 0
            }
        );
    }
}