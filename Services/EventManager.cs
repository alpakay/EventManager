using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class EventService : IEventService
    {
        private readonly IRepositoryManager _repositoryManager;

        public EventService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public IQueryable<Event> GetAllEvents(bool trackChanges)
        {
            return _repositoryManager.Event.GetAllEvents(trackChanges);
        }

        public Event? GetOneEvent(int eventId, bool trackChanges)
        {
            var eventItem = _repositoryManager.Event.GetOneEvent(eventId, trackChanges);
            if (eventItem == null)
            {
                throw new KeyNotFoundException($"Event with ID {eventId} not found.");
            }
            return eventItem;
        }
    }
}