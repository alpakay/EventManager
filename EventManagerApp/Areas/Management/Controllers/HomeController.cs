using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace EventManagerApp.Areas.Management.Controllers;

[Area("Management")]
public class HomeController : Controller
{
    private readonly IServiceManager _manager;
    public HomeController(IServiceManager manager)
    {
        _manager = manager;
    }
    public IActionResult Index(int userId)
    {
        var model = _manager.UserService.GetOneUser(userId, false);
        if (model == null)
        {
            return NotFound("User not found");
        }
        return View(model);
    }
}
