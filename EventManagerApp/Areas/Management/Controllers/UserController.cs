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
    public UserController(IServiceManager manager)
    {
        _manager = manager;
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
        if (!ModelState.IsValid)
        {
            return View(userLoginDto);
        }
        var result = await _manager.AuthService.LoginAsync(userLoginDto);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(userLoginDto);
        }
        return RedirectToAction("Index", "Home", new { area = "Management" });
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
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _manager.AuthService.LogoutAsync();
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
                userProfileDto.IsEditMode = true;
                return View("UserForm", userProfileDto);
            }
            _manager.UserService.UpdateUser(CurrentUserId, userProfileDto);
            return RedirectToAction("Index", "Home", new { area = "Management" });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            userProfileDto.IsEditMode = true;
            return View("UserForm", userProfileDto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Delete()
    {
        _manager.UserService.DeleteUser(CurrentUserId);
        await _manager.AuthService.LogoutAsync();
        return RedirectToAction("Index", "Home", new { area = "Management" });
    }
}
