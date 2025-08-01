using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class EventService : IEventService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EventService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void CreateEvent(EventFormDto eventEntity)
        {
            var eventMapped = _mapper.Map<Event>(eventEntity);
            eventMapped.CreatedAt = DateTime.UtcNow;
            
            _repositoryManager.Event.Create(eventMapped);
            _repositoryManager.Save();
        }

        public void DeleteEvent(int eventId)
        {
            var eventToDelete = _repositoryManager.Event.GetOneEvent(eventId, false);
            if (eventToDelete == null)
            {
                throw new KeyNotFoundException($"Event with ID {eventId} not found.");
            }
            _repositoryManager.Event.Delete(eventToDelete);
            _repositoryManager.Save();
        }

        public IQueryable<EventShowDto> GetAllEvents(bool trackChanges)
        {
            var events = _repositoryManager.Event.GetAllEvents(trackChanges);
            var eventsMapped = _mapper.Map<List<EventShowDto>>(events);
            return eventsMapped.AsQueryable();
        }

        public EventFormDto? GetOneEvent(int eventId, bool trackChanges)
        {
            var eventItem = _repositoryManager.Event.GetOneEvent(eventId, trackChanges);
            var eventMapped = _mapper.Map<EventFormDto>(eventItem);
            if (eventItem == null)
            {
                throw new KeyNotFoundException($"Event with ID {eventId} not found.");
            }
            return eventMapped;
        }

        public void UpdateEvent(EventFormDto eventEntity)
        {
            var eventToUpdate = _repositoryManager.Event.GetOneEvent(eventEntity.EventId, true);
            if (eventToUpdate == null)
            {
                throw new KeyNotFoundException($"Event with ID {eventEntity.EventId} not found.");
            }



            _mapper.Map(eventEntity, eventToUpdate);
            _repositoryManager.Event.Update(eventToUpdate);
            _repositoryManager.Save();
        }
    }
}