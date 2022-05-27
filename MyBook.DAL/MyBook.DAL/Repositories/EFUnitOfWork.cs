using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBook.DAL.EF;
using MyBook.DAL.Entities;
using MyBook.DAL.Interfaces;

namespace MyBook.DAL.Repositories;

public class EFUnitOfWork : IUnitOfWork
{
    private DatabaseContext _db;
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<ApplicationUser> _userManager;
    
    private AuthorRepository _authorRepository;
    private BookRepository _bookRepository;
    private RatingRepository _ratingRepository;
    private SubscriptionRepository _subscriptionRepository;
    private UserRepository _userRepository;
    private RoleRepository _roleRepository;

    public EFUnitOfWork(DbContextOptions<DatabaseContext> options)
    {
        _db = new DatabaseContext(options);

    }
    public IRepository<Author> Authors
    {
        get {
            if (_authorRepository == null)
                _authorRepository = new AuthorRepository(_db);
            return _authorRepository;
        }
    }

    public IRepository<Book> Books
    {
        get
        {
            if (_bookRepository == null)
                _bookRepository = new BookRepository(_db);
            return _bookRepository;
        }
    }

    public IRepository<Rating> Ratings
    {
        get
        {
            if (_ratingRepository == null)
                _ratingRepository = new RatingRepository(_db);
            return _ratingRepository;
        }
    }

    public IRepository<Subscription> Subscriptions
    {
        get
        {
            if (_subscriptionRepository == null)
                _subscriptionRepository = new SubscriptionRepository(_db);
            return _subscriptionRepository;
        }
    }
    
    public IUserRepository Users
    {
        get
        {
            if (_userRepository == null)
                _userRepository = new UserRepository(_userManager,_db);
            return _userRepository;
        }
    }
    public IRoleRepository Roles
    {
        get
        {
            if (_roleRepository == null)
                _roleRepository = new RoleRepository(_roleManager, _userManager, _db);
            return _roleRepository;
        }
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            this.disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}