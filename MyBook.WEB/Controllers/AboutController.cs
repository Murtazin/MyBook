using Microsoft.AspNetCore.Mvc;

namespace MyBook.WEB.Controllers;

public class AboutController : Controller
{
    // GET
    public IActionResult Index() => 
        View();
    
    public IActionResult Payments() => 
        View();
}