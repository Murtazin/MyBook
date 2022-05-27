using Microsoft.EntityFrameworkCore;
using MyBook.DAL.EF;
using MyBook.DAL.Entities;
using MyBook.DAL.Interfaces;

namespace MyBook.DAL.Repositories;

public class BookRepository : IRepository<Book>
{
    private readonly DatabaseContext _db;
    
    public BookRepository(DatabaseContext context)
    {
        _db = context;
    }
    
    public async Task<IEnumerable<Book>> GetAll()
    {
        return await _db.Books.ToListAsync();
    }

    public async Task<Book> Get(Guid id)
    {
        return await _db.Books.FindAsync(id);
    }

    public async Task Create(Book book)
    {
        await _db.Books.AddAsync(book);
    }

    public async Task Update(Book book)
    {
        await Task.Run(() =>
        {
            _db.Entry(book).State = EntityState.Modified;
        });
            
    }

    public async Task<IEnumerable<Book>> Find(Func<Book, Boolean> predicate)
    {
        await Task.Run(() =>
        {
            return _db.Books.Include(o => o.Title).Where(predicate).ToList();
        });
        return null;
    }

    public async Task Delete(Guid id)
    {
        await Task.Run(() =>
        {
            Book book = _db.Books.Find(id);
            if (book != null)
                _db.Books.Remove(book);
        });
            
    }
}