using Entities.Models;

namespace Services.Contracts
{
    public interface IEventService
    {
        IQueryable<Event> GetAllEvents(bool trackChanges);
        Event? GetOneEvent(int eventId, bool trackChanges);
        void CreateEvent(Event eventEntity);
    }
}