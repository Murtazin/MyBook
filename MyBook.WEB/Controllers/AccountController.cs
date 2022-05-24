using Microsoft.AspNetCore.Mvc;

namespace MyBook.WEB.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}