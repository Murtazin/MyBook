using Microsoft.AspNetCore.Identity;

namespace MyBook.DAL.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<IdentityRole>> GetAll();
    Task<IdentityRole> Get(string id);
    Task Create(IdentityRole name);
    Task Update(IdentityRole role);
    Task Delete(IdentityRole name);
    Task GiveRoles(List<string> role, string user);
    Task TakeAwayRoles(List<string> role, string user);
}