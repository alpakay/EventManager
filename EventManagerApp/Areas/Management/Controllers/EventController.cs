using Entities.Dtos;
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

        public IActionResult Create()
        {
            return View("EventForm", new EventFormDto
            {
                IsEditMode = false
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventFormDto model)
        {
            if (!ModelState.IsValid)
            {
                return View("EventForm", model);
            }
            model.CreatorId = CurrentUserId;
            _manager.EventService.CreateEvent(model);
            TempData["SuccessMessage"] = "Etkinlik başarıyla oluşturuldu.";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var eventEntity = _manager.EventService.GetOneEvent(id, false);
            eventEntity.IsEditMode = true;
            return View("EventForm", eventEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] EventFormDto model)
        {
            if (!ModelState.IsValid)
            {
                return View("EventForm", model);
            }
            model.CreatorId = CurrentUserId;
            _manager.EventService.UpdateEvent(model);
            TempData["SuccessMessage"] = "Etkinlik başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _manager.EventService.DeleteEvent(id);
            TempData["SuccessMessage"] = "Etkinlik başarıyla silindi.";
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var eventEntity = _manager.EventService.GetOneEvent(id, false);
            return View(eventEntity);
        }
    }
}