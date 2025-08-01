using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts
{
    public interface IEventService
    {
        IQueryable<EventShowDto> GetAllEvents(bool trackChanges);
        EventFormDto GetOneEvent(int eventId, bool trackChanges);
        void CreateEvent(EventFormDto eventEntity);
        void UpdateEvent(EventFormDto eventEntity);
        void DeleteEvent(int eventId);
    }
}