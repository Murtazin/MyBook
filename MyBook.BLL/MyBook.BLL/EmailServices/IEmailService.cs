namespace MyBook.BLL.EmailServices;

public interface IEmailService
{
    void SendEmail(Message message);
    Task SendEmailAsync(Message message);

}