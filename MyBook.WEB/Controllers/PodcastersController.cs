using Microsoft.AspNetCore.Mvc;

namespace MyBook.WEB.Controllers;

public class PodcastersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}