using Microsoft.EntityFrameworkCore;
using MyBook.DAL.EF;
using MyBook.DAL.Entities;
using MyBook.DAL.Interfaces;

namespace MyBook.DAL.Repositories;

public class SubscriptionRepository : IRepository<Subscription>
{
    private readonly DatabaseContext _db;
    
    public SubscriptionRepository(DatabaseContext context)
    {
        _db = context;
    }
    
    public async Task<IEnumerable<Subscription>> GetAll()
    {
        return await _db.Subscriptions.ToListAsync();
    }

    public async Task<Subscription> Get(Guid id)
    {
        return await _db.Subscriptions.FindAsync(id);
    }

    public async Task Create(Subscription subscription)
    {
        await _db.Subscriptions.AddAsync(subscription);
    }

    public async Task Update(Subscription subscription)
    {
        await Task.Run(() =>
        {
            _db.Entry(subscription).State = EntityState.Modified;
        });
    }

    public async Task<IEnumerable<Subscription>> Find(Func<Subscription, Boolean> predicate)
    {
        await Task.Run(() =>
        {
            return _db.Subscriptions.Include(o => o.Id).Where(predicate).ToList();
        });
        return null;
    }

    public async Task Delete(Guid id)
    {
        await Task.Run(() =>
        {
            Subscription subscription = _db.Subscriptions.Find(id);
            if (subscription != null)
                _db.Subscriptions.Remove(subscription);
        });
    }
}