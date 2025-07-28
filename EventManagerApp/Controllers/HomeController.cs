using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace EventManagerApp.Controllers;

public class HomeController : Controller
{
    private readonly IRepositoryManager _repositoryManager;

    public HomeController(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public IActionResult Index()
    {
        var model = _repositoryManager.Event.GetAllEvents(false);
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
