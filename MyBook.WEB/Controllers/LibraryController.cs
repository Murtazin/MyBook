using Microsoft.AspNetCore.Mvc;

namespace MyBook.WEB.Controllers;

public class LibraryController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}