using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace EventManagerApp.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class EventController : BaseController
    {
        private readonly IServiceManager _manager;
        public EventController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Etkinlik Listesi";
            ViewData["User"] = CurrentUserName;
            var model = _manager.EventService.GetAllEvents(true);
            return View(model);
        }

        public IActionResult Create(int id)
        {
            // Logic to get event details by id
            return View();
        }

        public IActionResult Edit(int id)
        {
            // Logic to get event details by id for editing
            return View();
        }

        public IActionResult Details(int id)
        {
            // Logic to get event details by id
            return View();
        }
    }
}