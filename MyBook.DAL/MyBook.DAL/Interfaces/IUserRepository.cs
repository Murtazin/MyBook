using MyBook.DAL.EF;

namespace MyBook.DAL.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> Get(string id);
        Task<IEnumerable<ApplicationUser>> Find(Func<ApplicationUser, Boolean> predicate);
        Task Create(ApplicationUser item);
        Task Update(ApplicationUser item);
        Task<IEnumerable<string>> GetRoles(string userName);
        Task Delete(Guid id);
}