using Microsoft.AspNetCore.Mvc;

namespace MyBook.WEB.Controllers;

public class AuthorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}