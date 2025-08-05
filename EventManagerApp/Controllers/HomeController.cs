using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace EventManagerApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Event", new { area = "" });
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
