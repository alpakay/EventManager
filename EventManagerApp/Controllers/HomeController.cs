using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace EventManagerApp.Controllers;

public class HomeController : Controller
{
    private readonly IServiceManager _manager;

    public HomeController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        var model = _manager.EventService.GetAllEvents(false);
        var oneEvent = _manager.EventService.GetOneEvent(2, false);
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
