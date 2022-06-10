using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess.Seed;

public partial class Seeds
{
    public static void CreateBookUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasMany(p => p.Users)
            .WithMany(p => p.FavoriteBooks)
            .UsingEntity(j => j.HasData(
                new
                {
                    FavoriteBooksId = new Guid("3cb92c37-ec67-4720-af23-d7f4d4096109"),
                    UsersId =Guid.Parse("4bee3a36-db98-4071-ad61-a61db810decb")
                }
            ));
    }
}