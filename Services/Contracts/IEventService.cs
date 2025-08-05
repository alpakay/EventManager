using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface IEventService
    {
        IQueryable<EventShowDto> GetAllEvents(bool trackChanges);
        IQueryable<EventShowDto> GetAllActiveEvents(bool trackChanges);
        EventFormDto GetOneEvent(int eventId, bool trackChanges);
        Event GetEventDetails(int eventId, bool trackChanges);
        void CreateEvent(EventFormDto eventEntity);
        void UpdateEvent(EventFormDto eventEntity);
        void DeleteEvent(int eventId, string rootPath);
    }
}