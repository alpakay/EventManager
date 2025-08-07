using Entities.Dtos;
using Entities.Models;

namespace EventManagerApp.ViewModels
{
    public class EventDetailsViewModel
    {
        public Event MainEvent { get; set; }
        public IEnumerable<EventShowDto> OtherEvents { get; set; }
    }
}