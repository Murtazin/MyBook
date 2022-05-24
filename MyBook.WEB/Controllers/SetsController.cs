using Microsoft.AspNetCore.Mvc;

namespace MyBook.WEB.Controllers;

public class SetsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}