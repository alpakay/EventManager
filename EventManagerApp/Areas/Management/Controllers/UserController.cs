using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace EventManagerApp.Areas.Management.Controllers;

[Area("Management")]
public class UserController : BaseController
{

    private readonly IServiceManager _manager;
    private readonly IWebHostEnvironment _env;
    public UserController(IServiceManager manager, IWebHostEnvironment env)
    {
        _manager = manager;
        _env = env;
    }

    [Authorize]
    public IActionResult Index()
    {
        ViewData["Title"] = "Kullanıcı Listesi";
        ViewData["User"] = CurrentUserName;
        var users = _manager.UserService.GetAllUsers(false);
        return View(users);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] UserLoginDto userLoginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginDto);
            }
            var result = await _manager.AuthService.LoginAsync(userLoginDto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Unknown error");
                return View(userLoginDto);
            }
            TempData["SuccessMessage"] = "Giriş başarılı.";
            return RedirectToAction("Index", "Home", new { area = "Management" });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(userLoginDto);
        }
    }

    public IActionResult Register()
    {
        var model = new UserProfileDto
        {
            IsEditMode = false,
            FullName = string.Empty,
            Email = string.Empty,
            Password = string.Empty,
            ConfirmPassword = string.Empty
        };
        return View("UserForm", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] UserProfileDto userRegisterDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View("UserForm", userRegisterDto);
            }
            await _manager.AuthService.RegisterAsync(userRegisterDto);
            TempData["SuccessMessage"] = "Kayıt başarılı. Lütfen giriş yapın.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("UserForm", userRegisterDto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _manager.AuthService.LogoutAsync();
        TempData["SuccessMessage"] = "Çıkış başarılı.";
        return RedirectToAction("Login", "User", new { area = "Management" });
    }

    [Authorize]
    public IActionResult Profile()
    {
        ViewData["Title"] = "Kullanıcı Profili";
        ViewData["User"] = CurrentUserName;
        var user = _manager.UserService.GetOneUser(CurrentUserId, false);
        if (user == null)
        {
            return NotFound("User not found");
        }
        var userProfile = new UserProfileDto
        {
            FullName = user.FullName,
            Email = user.Email,
            BirthDate = user.BirthDate,
            Password = string.Empty,
            ConfirmPassword = string.Empty,
            IsEditMode = true
        };
        return View("UserForm", userProfile);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Update([FromForm] UserProfileDto userProfileDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View("UserForm", userProfileDto);
            }
            _manager.UserService.UpdateUser(CurrentUserId, userProfileDto);
            TempData["SuccessMessage"] = "Profil güncellendi.";
            return RedirectToAction("Index", "Home", new { area = "Management" });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("UserForm", userProfileDto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Delete()
    {
        var rootPath = _env.WebRootPath;
        _manager.UserService.DeleteUser(CurrentUserId, rootPath);
        await _manager.AuthService.LogoutAsync();
        TempData["SuccessMessage"] = "Kullanıcı silindi.";
        return RedirectToAction("Index", "Home", new { area = "Management" });
    }
}
