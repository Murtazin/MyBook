using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBook.DAL.Entities;

namespace MyBook.DAL.EF;

public class DatabaseContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Authors
        builder.Entity<Author>().HasData(
            new Author[]
            {
                new Author{Age = 28, FirstName = "Марк", SecondName = "Мэнсон", Id = Guid.NewGuid()},
                new Author{Age = 56, FirstName = "Борис", SecondName = "Акунин", Id = Guid.NewGuid()},
                new Author{Age = 48, FirstName = "Дэн", SecondName = "Браун", Id = Guid.NewGuid()},
                new Author{Age = 39, FirstName = "Ю", SecondName = "Несбё", Id = Guid.NewGuid()},
                new Author{Age = 18, FirstName = "Сара", SecondName = "Джио", Id = Guid.NewGuid()}
            }
        );
        // Books
        builder.Entity<Book>().HasData(
            new Book[]
            {
                new Book
                {
                    Author = new Author{Age = 28, FirstName = "Марк", SecondName = "Мэнсон", Id = Guid.NewGuid()},
                    CountOfPages = 200,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut volutpat nisl non neque pellentesque convallis. Maecenas tincidunt lacus id finibus rhoncus. Donec molestie, leo quis rutrum tristique, lacus ex mollis neque, eu pharetra metus lacus sed turpis. Nulla dictum eu sem id rutrum. Maecenas bibendum fermentum est, et vulputate nulla viverra et. Sed quis ligula vitae urna egestas condimentum. Morbi pellentesque molestie magna, et sollicitudin quam porta non. Vivamus quis elementum justo. Aliquam at ipsum pharetra, suscipit quam sit amet, feugiat neque.",
                    Id = Guid.NewGuid(),
                    Title = "Book #1",
                    YearOfIssue = new DateTime(2020, 02, 05)
                },
                new Book
                {
                    Author = new Author{Age = 56, FirstName = "Борис", SecondName = "Акунин", Id = Guid.NewGuid()},
                    CountOfPages = 300,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut volutpat nisl non neque pellentesque convallis. Maecenas tincidunt lacus id finibus rhoncus. Donec molestie, leo quis rutrum tristique, lacus ex mollis neque, eu pharetra metus lacus sed turpis. Nulla dictum eu sem id rutrum. Maecenas bibendum fermentum est, et vulputate nulla viverra et. Sed quis ligula vitae urna egestas condimentum. Morbi pellentesque molestie magna, et sollicitudin quam porta non. Vivamus quis elementum justo. Aliquam at ipsum pharetra, suscipit quam sit amet, feugiat neque.",
                    Id = Guid.NewGuid(),
                    Title = "Book #2",
                    YearOfIssue = new DateTime(2021, 02, 05)
                },
                new Book
                {
                    Author = new Author{Age = 48, FirstName = "Дэн", SecondName = "Браун", Id = Guid.NewGuid()},
                    CountOfPages = 400,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut volutpat nisl non neque pellentesque convallis. Maecenas tincidunt lacus id finibus rhoncus. Donec molestie, leo quis rutrum tristique, lacus ex mollis neque, eu pharetra metus lacus sed turpis. Nulla dictum eu sem id rutrum. Maecenas bibendum fermentum est, et vulputate nulla viverra et. Sed quis ligula vitae urna egestas condimentum. Morbi pellentesque molestie magna, et sollicitudin quam porta non. Vivamus quis elementum justo. Aliquam at ipsum pharetra, suscipit quam sit amet, feugiat neque.",
                    Id = Guid.NewGuid(),
                    Title = "Book #3",
                    YearOfIssue = new DateTime(2022, 02, 05)
                }
            }
        );
    }
}