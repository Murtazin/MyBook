using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBook.DAL.EF;
using MyBook.DAL.Interfaces;

namespace MyBook.DAL.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly DatabaseContext _db;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleRepository(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, DatabaseContext db)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
    }

    public async Task<IEnumerable<IdentityRole>> GetAll()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<IdentityRole> Get(string id)
    {
        return await _db.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task Create(IdentityRole role)
    {
        await _roleManager.CreateAsync(role);
    }
    public async Task Update(IdentityRole role)
    {
        await Task.Run(() =>
        {
            _db.Entry(role).State = EntityState.Modified;
        });
    }
    public async Task GiveRoles(List<string> role, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        await _userManager.AddToRolesAsync(user, role);
    }
    public async Task TakeAwayRoles(List<string> role, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        await _userManager.RemoveFromRolesAsync(user, role);
    }
    public async Task Delete(IdentityRole role)
    {
        await _roleManager.DeleteAsync(role);
    }
}