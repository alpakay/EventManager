using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace EventManagerApp.Areas.Management.Controllers;

[Area("Management")]
public class HomeController : BaseController
{
    private readonly IServiceManager _manager;
    public HomeController(IServiceManager manager)
    {
        _manager = manager;
    }

    [Authorize]
    public IActionResult Index()
    {

        var model = _manager.UserService.GetOneUser(CurrentUserId, true);
        if (model == null)
        {
            return HandleNotFound("User not found");
        }
        ViewBag.WelcomeMessage = $"Hoş Geldiniz, {model.FullName}!";
        return View(model);
    }
}
