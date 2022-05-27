using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBook.DAL.EF;
using MyBook.DAL.Interfaces;

namespace MyBook.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    public UserRepository(UserManager<IdentityUser> userManager, DatabaseContext context)
    {
        _db = context;
        _userManager = userManager;
    }

    public async Task<IEnumerable<IdentityUser>> GetAll()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<IdentityUser> Get(string id)
    {
        return await _userManager.FindByIdAsync(id); ;
    }

    public async Task Create(IdentityUser userSubscription)
    {
        await _db.Users.AddAsync(userSubscription);
    }

    public async Task Update(IdentityUser userSubscription)
    {
        await Task.Run(() =>
        {
            _db.Entry(userSubscription).State = EntityState.Modified;

        });
    }
    public async Task<IEnumerable<IdentityUser>> Find(Func<IdentityUser, Boolean> predicate)
    {
        await Task.Run(() =>
        {
            return _db.Users.Where(predicate).ToList();

        });
        return null;
            
    }

    public async Task<IEnumerable<string>> GetRoles(string userName)
    {

        var user = _db.Users.FirstOrDefault(u => u.UserName == userName);

        return await _userManager.GetRolesAsync(user);
    }

    public async Task Delete(Guid id)
    {
        await Task.Run(() =>
        {
            IdentityUser users = _db.Users.Find(id);
            if (users != null)
                _db.Users.Remove(users);

        });
            
    }
}