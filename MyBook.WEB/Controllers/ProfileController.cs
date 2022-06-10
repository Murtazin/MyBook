using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DAL.Contexts;
using MyBook.DAL.Entities;
using MyBook.WEB.Models;

namespace MyBook.WEB.Controllers;

[Authorize(Roles = "User")]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<ProfileController> _logger;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationContext _context;

    public ProfileController(UserManager<User> userManager, ILogger<ProfileController> logger,
        SignInManager<User> signInManager, ApplicationContext context)
    {
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        if (user == null)
            return NotFound();
        
        var sub = (await _context.Subs.FirstOrDefaultAsync(x => x.Id == user.SubId))!;

        var model = new EditProfileViewModel
        {
            Email = user.Email, Name = user.Name, LastName = user.LastName, Image = user.Image,
            Sub = sub,
            SubDurationLeft = sub.Duration - DateTime.Now.Subtract(user.SubDateStart).Days
        };
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name);
            if (user != null)
            {
                var result =
                    await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);


                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Пароль изменён успешно!");
                    _logger.LogInformation("Пользователь \"{User}\" изменил свой пароль", user.UserName);
                }
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
            else
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> ChangeImage(ChangeProfileImageViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user != null)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                    imageData = binaryReader.ReadBytes((int) model.Image.Length);

                user.Image = Convert.ToBase64String(imageData);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Изменения сохранены!");
                    _logger.LogInformation(
                        "Пользователь \"{User}\" обновил фотографию профиля", user.Email);
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<ActionResult> ResetImage()
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user != null)
            {
                user.Image = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync("wwwroot/img/user.png"));

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Изменения сохранены!");
                    _logger.LogInformation(
                        "Пользователь \"{User}\" обновил фотографию профиля", user.Email);
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(EditProfileViewModel model)
    {
        ModelState.Remove("Image");
        ModelState.Remove("Sub");
        ModelState.Remove("SubDurationLeft");
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user != null)
            {
                var sub = (await _context.Subs.FirstOrDefaultAsync(x => x.Id == user.SubId))!;
                var oldEmail = user.Email;
   
                user.Email = model.Email;
                user.UserName = model.Email;
                user.Name = model.Name;
                user.LastName = model.LastName;
                
                //roflan
                model.Image = user.Image;
                model.Sub = sub;
                model.SubDurationLeft = sub.Duration - DateTime.Now.Subtract(user.SubDateStart).Days;

                var result = await _userManager.UpdateAsync(user);
                
                if (oldEmail != user.Email)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }

                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Изменения сохранены!");
                    _logger.LogInformation(
                        "Пользователь \"{User}\" обновил информацию профиля ({Email}, {Name}, {LastName})",
                        oldEmail, user.Email, user.Name, user.LastName);
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
        }

        return View("Index", model);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToFavorites(Guid id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        var user = await _context.Users.Include(x => x.FavoriteBooks)
            .FirstOrDefaultAsync(x => x.UserName == User.Identity!.Name);

        string result;
        if (book is not null && user is not null)
        {
            if (!user.FavoriteBooks.Contains(book))
            {
                user.FavoriteBooks.Add(book);
                await _context.SaveChangesAsync();
                result = "Книга успешно добавлена в \"мои книги\"";
            }
            else
                result = "Книга уже была добавлена в \"мои книги\"";
        }
        else
            result = "Что-то пошло не так";
        
        return Json(new {msg = result});
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromFavorites(Guid id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        var user = await _context.Users.Include(x => x.FavoriteBooks)
            .FirstOrDefaultAsync(x => x.UserName == User.Identity!.Name);

        string result;
        if (book is not null || user is not null)
        {
            if (user!.FavoriteBooks.Contains(book!))
            {
                user.FavoriteBooks.Remove(book!);
                await _context.SaveChangesAsync();
                result = "Книга успешно удалена из \"моих книг\"";
            }
            else
                result = "Книга не находится в \"моих книгах\"";
        }
        else
            result = "Что-то пошло не так";

        return Json(new {msg = result});
    }


    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        var user = await _context.Users.Include(x => x.FavoriteBooks).ThenInclude(x=> x.Author)
            .FirstOrDefaultAsync(x => x.UserName == User.Identity!.Name);
        
        if (user is not null)
            return View(user.FavoriteBooks);

        return RedirectToAction("PageNotFound", "Home");
    }
}