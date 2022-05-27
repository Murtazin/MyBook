namespace MyBook.BLL.DTOModels;

public class UserDTO
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnable { get; set; }
    public bool LockoutEnable { get; set; }
    public string Avatar { get; set; }
}