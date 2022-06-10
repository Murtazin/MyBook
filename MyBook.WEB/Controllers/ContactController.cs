using Microsoft.AspNetCore.Mvc;
using MyBook.Models;
using MyBook.Services.EmailServices;

namespace MyBook.WEB.Controllers;
public class ContactController : Controller
{
    private readonly IEmailService _emailService;
    private readonly EmailConfiguration _emailConfig;
    
    public ContactController(IEmailService emailService, EmailConfiguration emailConfig)
    {
        _emailService = emailService;
        _emailConfig = emailConfig;
    }

    [HttpGet]
    public IActionResult Index() =>
        View();
    
    [HttpPost]
    public async Task<IActionResult> SendAsync(ContactViewModel model)
    {
        if (ModelState.IsValid)
        {
            var message = new Message(new[] {_emailConfig.From}, $"Обращение: {model.Subject}",
                $"Имя: {model.Name}\nEmail: {model.Email}\n\n{model.Message}");
            try
            {
                await _emailService.SendEmailAsync(message);
                
                ModelState.AddModelError("", "Обращение отправлено успешно!");
            }
            catch (MailKit.Net.Smtp.SmtpProtocolException)
            {
                ModelState.AddModelError("", "Сервис по отправке обращений временно не работает.");
            }
        }

        return View("Index", model);
    }
}