using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Entities.Dtos;
using EventManagerApp.Areas.Management.ViewModels;
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
        private readonly IWebHostEnvironment _env;
        public EventController(IServiceManager manager, IWebHostEnvironment env)
        {
            _manager = manager;
            _env = env;
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
            return View("EventForm", new EventFormViewModel
            {
                Event = new EventFormDto { IsEditMode = false },
                ImageFile = null!
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("EventForm", model);
                }
                model.Event.CreatorId = CurrentUserId;
                var rootPath = _env.WebRootPath;
                var fileName = await _manager.FileService.UploadFileAsync(model.ImageFile, rootPath);
                model.Event.ImgUrl = fileName;
                _manager.EventService.CreateEvent(model.Event);
                TempData["SuccessMessage"] = "Etkinlik başarıyla oluşturuldu.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("EventForm", model);
            }
        }

        public IActionResult Edit(int id)
        {
            var eventEntity = _manager.EventService.GetOneEvent(id, false);
            eventEntity.IsEditMode = true;
            var viewModel = new EventFormViewModel
            {
                Event = eventEntity,
                ImageFile = null!
            };
            return View("EventForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EventFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("EventForm", model);
                }
                if (model.ImageFile != null)
                {
                    var rootPath = _env.WebRootPath;
                    var fileName = await _manager.FileService.UploadFileAsync(model.ImageFile, _env.WebRootPath);
                    _manager.FileService.DeleteFile(model.Event.ImgUrl, rootPath);
                    model.Event.ImgUrl = fileName;
                }
                _manager.EventService.UpdateEvent(model.Event);
                TempData["SuccessMessage"] = "Etkinlik başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("EventForm", model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var rootPath = _env.WebRootPath;
            _manager.EventService.DeleteEvent(id, rootPath);
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