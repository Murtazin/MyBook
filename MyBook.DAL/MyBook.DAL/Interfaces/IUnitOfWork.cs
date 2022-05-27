using MyBook.DAL.Entities;

namespace MyBook.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Author> Authors { get; }
    IRepository<Subscription> Subscriptions { get; }
    IRepository<Book> Books { get; }
    IRepository<Rating> Ratings { get; }
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    
    void Save();
}