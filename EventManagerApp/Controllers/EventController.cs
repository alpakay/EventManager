using EventManagerApp.ViewModels;
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
        var events = _serviceManager.EventService.GetAllActiveEvents(false);
        return View(events);
    }

    public IActionResult Details(int id)
    {
        var eventDetails = _serviceManager.EventService.GetEventDetails(id, false);
        var otherEvents = _serviceManager.EventService.GetLastFiveActiveEvents(id, false);
        var eventDetailsViewModel = new EventDetailsViewModel
        {
            MainEvent = eventDetails,
            OtherEvents = otherEvents
        };
        return View(eventDetailsViewModel);
    }

    public IActionResult Calendar()
    {
        return View();
    }

    public IActionResult GetEventsForCalendar()
    {
        var events = _serviceManager.EventService.GetAllActiveEvents(false)
            .Select(e => new
            {
                id = e.EventId,
                title = FormatEventTitle(e.Name, e.StartDate),
                start = e.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = e.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                url = Url.Action("Details", "Event", new { Area = "", id = e.EventId }),
                allDay = false,
            });

        return Json(events);
    }

    private string FormatEventTitle(string name, DateTime startDate)
    {
        return $"{startDate:HH:mm} - {name}";
    }
}