using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace EventManagerApp.Areas.Management.Controllers;

[Area("Management")]
public class UserController : Controller
{

    private readonly IServiceManager _manager;
    public UserController(IServiceManager manager)
    {
        _manager = manager;
    }
    public IActionResult Index()
    {
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
            ModelState.AddModelError("", "Invalid login attempt");
            return View(userLoginDto);
        }
        return RedirectToAction("Index", "Home", new { area = "Management" });
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register([FromForm] UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return View(userRegisterDto);
        }
        _manager.UserService.CreateUser(userRegisterDto);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _manager.AuthService.LogoutAsync();
        return RedirectToAction("Login", "User", new { area = "Management" });
    }

    public IActionResult Profile([FromRoute] int userId)
    {
        var user = _manager.UserService.GetOneUser(userId, false);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return View("Register", user);
    }
}
