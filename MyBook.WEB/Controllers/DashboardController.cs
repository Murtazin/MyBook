using Microsoft.AspNetCore.Mvc;

namespace MyBook.WEB.Controllers;

public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}