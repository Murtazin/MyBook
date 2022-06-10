using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBook.BLL.EmailServices;
using MyBook.DAL.Entities;

namespace MyBook.WEB.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEmailService _emailService;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult Register() =>
        View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                SubId = 4,
                SubDateStart = default,
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                LastName = model.LastName,
                Image = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync("wwwroot/img/user.png")),
                EmailConfirmed = false,
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.AddToRoleAsync(user, "User");

                var link = Url.Action(nameof(VerifyEmail), "Auth", new {userId = user.Id, code}, Request.Scheme,
                    Request.Host.ToString());

                var message = new Message(new[] {model.Email}, "Подтверждение почты",
                    $"<h2>Добро пожаловать на MyBook!</h2><br><p>Пожалуйста, подтвердите свою почту, перейдя по ссылке</p><a href='{link}'>Подтвердить регистрацию</a>");

                await _emailService.SendEmailAsync(message);

                return View("AuthStatus", "Письмо с подтверждением было отправлено на вашу почту");
            }
            else
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
    
    public async Task<IActionResult> VerifyEmail(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return RedirectToAction("PageNotFound", "Home");

        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (result.Succeeded)
            return View("AuthStatus", "Почта подтверждена! Теперь можно зайти");

        return RedirectToAction("PageNotFound", "Home");
    }
    
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string returnUrl)
    {
        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl,
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
    {
        model.ReturnUrl = returnUrl ?? Url.Content("~/");
        model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
        ModelState.Remove("ExternalLogins");
        ModelState.Remove("ReturnUrl");
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
        }

        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    [AllowAnonymous]
    [HttpPost]
    public IActionResult ExternalLogin(string provider, string returnUrl)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Auth",
            new { ReturnUrl = returnUrl });

        var properties =
            _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return new ChallengeResult(provider, properties);
    }

    [AllowAnonymous]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
    {
        returnUrl = returnUrl ?? Url.Content("~/");

        var loginViewModel = new LoginViewModel
        {
            ReturnUrl = returnUrl,
            ExternalLogins =
                (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        };

        if (remoteError != null)
        {
            ModelState.AddModelError(string.Empty, $"Ошибка со внешнего провайдера: {remoteError}");
            return View("Login", loginViewModel);
        }

        // Get the login information about the user from the external login provider
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при загрузке внешней информации для входа.");
            return View("Login", loginViewModel);
        }

        // If the user already has a login (i.e if there is a record in AspNetUserLogins
        // table) then sign-in the user with this external login provider
        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
            info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (signInResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        // If there is no record in AspNetUserLogins table, the user may not have
        // a local account
        else
        {
            // Get the email claim value
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            if (email != null)
            {
                // Create a new user without password if we do not have a user already
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new User
                    {
                        SubId = 4,
                        SubDateStart = default,
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                        Name = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                        LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                        Image = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync("wwwroot/img/user.png")),
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };

                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(user, "User");
                }

                // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return LocalRedirect(returnUrl);
            }

            // If we cannot find the user email we cannot continue
            ViewBag.ErrorTitle = $"Email не получен со внешнего провайдера: {info.LoginProvider}";
            ViewBag.ErrorMessage = "Пожалуйста, обратитесь к нам на почту: support@mybook.ru";

            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> RestoreAccess(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(PasswordReset), "Auth", new {email = user.Email, token}, Request.Scheme,
                Request.Host.ToString());

            var message = new Message(new[] {email}, "Восстановление доступа",
                "<p>Для восстановления доступа к аккаунту вам нужно перейти по ссылке, чтобы сменить ваш пароль." +
                $"<br><a href='{link}'>Сменить пароль</a>");
            try
            {
                await _emailService.SendEmailAsync(message);
                return View("AuthStatus", "Письмо с инструкцией отправлена на вашу почту");
            }
            catch (Exception)
            {
                return View("AuthStatus", "Сервис временно не работает.");
            }
        }

        return RedirectToAction("PageNotFound", "Home");
    }

    [HttpGet]
    public IActionResult RestoreAccess() =>
        View();


    [HttpGet]
    public IActionResult PasswordReset(string email, string token) =>
        View(new ResetPasswordViewModel
        {
            Email = email,
            Token = token
        });

    [HttpPost]
    public async Task<IActionResult> PasswordReset(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                    return View("AuthStatus", "Пароль изменён успешно");
                else
                    ModelState.AddModelError("", "Ошибка. Не удалось сменить пароль.");
            }
            else
                ModelState.AddModelError("", "Такого пользователя не существует");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult AuthStatus() =>
        View();
}