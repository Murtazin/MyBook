using Microsoft.EntityFrameworkCore;
using MyBook.DAL.EF;
using MyBook.DAL.Entities;
using MyBook.DAL.Interfaces;

namespace MyBook.DAL.Repositories;

public class RatingRepository : IRepository<Rating>
{
    private readonly DatabaseContext _db;
    
    public RatingRepository(DatabaseContext context)
    {
        _db = context;
    }
    
    public async Task<IEnumerable<Rating>> GetAll()
    {
        return await _db.Ratings.ToListAsync();
    }

    public async Task<Rating> Get(Guid id)
    {
        return await _db.Ratings.FindAsync(id);
    }

    public async Task Create(Rating rating)
    {
        await _db.Ratings.AddAsync(rating);
    }

    public async Task Update(Rating rating)
    {
        await Task.Run(() =>
        {
            _db.Entry(rating).State = EntityState.Modified;
        });
    }

    public async Task<IEnumerable<Rating>> Find(Func<Rating, Boolean> predicate)
    {
        await Task.Run(() =>
        {
            return _db.Ratings.Include(o => o.Id).Where(predicate).ToList();
        });
        return null;
    }

    public async Task Delete(Guid id)
    {
        await Task.Run(() =>
        {
            Rating rating = _db.Ratings.Find(id);
            if (rating != null)
                _db.Ratings.Remove(rating);
        });
    }
}