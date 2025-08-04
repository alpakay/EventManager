using Entities.Dtos;

namespace EventManagerApp.Areas.Management.ViewModels
{
    public class EventFormViewModel
    {
        public EventFormDto Event { get; set; } = new EventFormDto();
        public IFormFile ImageFile { get; set; } = null!;
    }
}