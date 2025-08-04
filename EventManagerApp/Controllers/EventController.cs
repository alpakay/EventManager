using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace EventManagerApp.Controllers;

public class EventController : Controller
{
    private readonly IServiceManager _serviceManager;
    public EventController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    public IActionResult Index()
    {
        var events = _serviceManager.EventService.GetAllEvents(false);
        return View(events);
    }

    public IActionResult Details(int id)
    {
        var eventDetails = _serviceManager.EventService.GetEventDetails(id, false);
        return View(eventDetails);
    }

    public IActionResult Calendar()
    {
        return View();
    }

    public IActionResult GetEventsForCalendar()
    {
        var events = _serviceManager.EventService.GetAllEvents(false)
            .Select(e => new
            {
                id = e.EventId,
                title = e.Name,
                start = e.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = e.EndDate.ToString("yyyy-MM-ddTHH:mm:ss")
            });

        return Json(events);
    }
}