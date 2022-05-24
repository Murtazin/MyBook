using Microsoft.AspNetCore.Mvc;

namespace MyBook.WEB.Controllers;

public class BooksController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}