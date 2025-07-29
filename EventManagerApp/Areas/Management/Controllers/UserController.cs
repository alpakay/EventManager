using Entities.Dtos;
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
    public IActionResult Login([FromForm] UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
        {
            return View(userLoginDto);
        }
        var result = _manager.UserService.Login(userLoginDto, false);
        if (result == 0)
        {
            ModelState.AddModelError("", "Invalid login attempt");
            return View(userLoginDto);
        }
        return RedirectToAction("Index");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register([FromForm]UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return View(userRegisterDto);
        }
        _manager.UserService.CreateUser(userRegisterDto);
        return RedirectToAction("Index");
    }
}
