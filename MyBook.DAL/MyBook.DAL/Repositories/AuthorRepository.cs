using Microsoft.EntityFrameworkCore;
using MyBook.DAL.EF;
using MyBook.DAL.Entities;
using MyBook.DAL.Interfaces;

namespace MyBook.DAL.Repositories;

public class AuthorRepository : IRepository<Author>
{
    private readonly DatabaseContext _db;
    
    public AuthorRepository(DatabaseContext context)
    {
        _db = context;
    }
    
    public async Task<IEnumerable<Author>> GetAll()
    {
        return await _db.Authors.ToListAsync();
    }

    public async Task<Author> Get(Guid id)
    {
        return await _db.Authors.FindAsync(id);
    }

    public async Task Create(Author author)
    {
        await _db.Authors.AddAsync(author);
    }

    public async Task Update(Author author)
    {
        await Task.Run(() =>
        {
            _db.Entry(author).State = EntityState.Modified;
        });
            
    }

    public async Task<IEnumerable<Author>> Find(Func<Author, Boolean> predicate)
    {
        await Task.Run(() =>
        {
            return _db.Authors.Include(o => o.FirstName).Where(predicate).ToList();
        });
        return null;
    }

    public async Task Delete(Guid id)
    {
        await Task.Run(() =>
        {
            Author author = _db.Authors.Find(id);
            if (author != null)
                _db.Authors.Remove(author);
        });
            
    }
}