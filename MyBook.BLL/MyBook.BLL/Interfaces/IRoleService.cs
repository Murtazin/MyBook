using MyBook.BLL.DTOModels;

namespace MyBook.BLL.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDTO>> GetRoles();
    Task<RoleDTO> Get(string id);

    Task Create(string role);
    Task Delete(string id);
    Task Update(RoleDTO editedRole);
    Task GiveRoles(List<string> role, string user);
    Task TakeAwayRoles(List<string> role, string user);
}