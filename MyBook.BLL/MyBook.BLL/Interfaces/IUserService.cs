using MyBook.BLL.DTOModels;

namespace MyBook.BLL.Interfaces;

public interface IUserService
{
    Task<UserDTO> GetUser(string id);
    Task<IEnumerable<UserDTO>> GetUsers();
    Task<IEnumerable<string>> GetRoles(string userName);
}