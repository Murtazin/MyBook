namespace MyBook.BLL.Services.EmailServices;

public interface IEmailService
{
    void SendEmail(Message message);
    Task SendEmailAsync(Message message);

}