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
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
