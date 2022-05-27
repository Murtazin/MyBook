using Microsoft.AspNetCore.Identity;
using MyBook.DAL.EF;

namespace MyBook.DAL.Interfaces;

public interface IUserRepository
{
        Task<IEnumerable<IdentityUser>> GetAll();
        Task<IdentityUser> Get(string id);
        // Task<IEnumerable<IdentityUser>> Find(Func<IdentityUser, Boolean> predicate);
        Task Create(IdentityUser item);
        Task Update(IdentityUser item);
        Task<IEnumerable<string>> GetRoles(string userName);
        Task Delete(Guid id);
}