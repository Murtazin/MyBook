using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DAL.Contexts;
using MyBook.DAL.Entities;
using MyBook.DAL.Identity;

namespace MyBook.WEB.Controllers;

public class SubController : Controller
{
    private readonly ApplicationContext _context;

    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public SubController(ApplicationContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index() =>
        View(await _context.Subs.Where(v => !v.Name.Equals("Бесплатно")).ToListAsync());

    public IActionResult Payment(int subId)
    {
        ViewData["subId"] = HttpContext.Request.Query["subId"].ToString();
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Pay(int subId)
    {
        var curUser = await _userManager.GetUserAsync(HttpContext.User);

        curUser.SubId = subId;
        curUser.SubDateStart = DateTime.Now;

        await _userManager.UpdateAsync(curUser);
        await _userManager.AddToRoleAsync(curUser, "UserSub");

        return RedirectToAction("PaymentResult", new {message = "Оплата прошла успешно!"});
    }

    [HttpGet]
    public async Task<IActionResult> ResetSub()
    {
        var curUser = await _userManager.GetUserAsync(HttpContext.User);

        curUser.SubDateStart = default;
        curUser.SubId = 4;
        await _userManager.RemoveFromRoleAsync(curUser, "UserSub");
        
        return RedirectToAction("PaymentResult", new {message = "Сброс подписки выполнен успешно"});
    }

    public IActionResult PaymentResult(string message)
    {
        ViewData["result"] = message;
        return View();
    }
}