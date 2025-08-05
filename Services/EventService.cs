using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class EventService : IEventService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public EventService(IRepositoryManager repositoryManager, IMapper mapper, IFileService fileService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _fileService = fileService;
        }
        public void CreateEvent(EventFormDto eventEntity)
        {
            var eventMapped = _mapper.Map<Event>(eventEntity);
            eventMapped.CreatedAt = DateTime.Now;

            _repositoryManager.Event.Create(eventMapped);
            _repositoryManager.Save();
        }

        public void DeleteEvent(int eventId, string rootPath)
        {
            var eventToDelete = _repositoryManager.Event.GetOneEvent(eventId, false);
            if (eventToDelete == null)
            {
                throw new KeyNotFoundException($"Event with ID {eventId} not found.");
            }
            _fileService.DeleteFile(eventToDelete.ImgUrl, rootPath);
            _repositoryManager.Event.Delete(eventToDelete);
            _repositoryManager.Save();
        }

        public IQueryable<EventShowDto> GetAllEvents(bool trackChanges)
        {
            UpdatePastEventsStatus();
            var events = _repositoryManager.Event.GetAllEvents(trackChanges);
            var eventsMapped = _mapper.Map<List<EventShowDto>>(events);
            return eventsMapped.AsQueryable();
        }

        public IQueryable<EventShowDto> GetAllActiveEvents(bool trackChanges)
        {
            UpdatePastEventsStatus();
            var events = _repositoryManager.Event.GetEventsByCondition(e => e.IsActive, trackChanges);
            var eventsMapped = _mapper.Map<List<EventShowDto>>(events);
            return eventsMapped.AsQueryable();
        }

        public Event GetEventDetails(int eventId, bool trackChanges)
        {
            var eventItem = _repositoryManager.Event.GetOneEvent(eventId, trackChanges);
            if (eventItem == null)
            {
                throw new KeyNotFoundException($"Event with ID {eventId} not found.");
            }
            return eventItem;
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

        private void UpdatePastEventsStatus()
        {
            var pastEvents = _repositoryManager.Event.GetEventsByCondition(e => e.EndDate < DateTime.Now && e.IsActive, false).ToList();
            foreach (var pastEvent in pastEvents)
            {
                pastEvent.IsActive = false;
                _repositoryManager.Event.Update(pastEvent);
            }
            _repositoryManager.Save();
        }
    }
}