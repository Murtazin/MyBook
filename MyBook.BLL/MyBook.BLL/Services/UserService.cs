using AutoMapper;
using System.ComponentModel.DataAnnotations;
using MyBook.BLL.DTOModels;
using MyBook.BLL.Interfaces;
using MyBook.DAL.EF;
using MyBook.DAL.Interfaces;

namespace MyBook.BLL.Services;

public class UserService : IUserService
{
    IUnitOfWork Database { get; set; }

    public UserService(IUnitOfWork database)
    {
        Database = database;
    }

    public async Task<UserDTO> GetUser(string id)
    {

        if (id == null)
            throw new ValidationException("Пользователь с таким id не найден");
        var user = await Database.Users.Get(id);
        if (user == null)
            throw new ValidationException("Пользователь не найден");

        return new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnable = user.TwoFactorEnabled,
            LockoutEnable = user.TwoFactorEnabled

        };
    }

    public async Task<IEnumerable<UserDTO>> GetUsers()
    {
        var users = await Database.Users.GetAll();
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDTO>()).CreateMapper();
        return mapper.Map<IEnumerable<ApplicationUser>, List<UserDTO>>(users);
    }

    public async Task<IEnumerable<string>> GetRoles(string userName)
    {

        return await Database.Users.GetRoles(userName);
    }
}