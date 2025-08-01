using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateEvent(Event eventEntity) => Create(eventEntity);

        public IQueryable<Event> GetAllEvents(bool trackChanges)
        {
            return trackChanges
                ? _context.Events.Include(e => e.Creator)
                : _context.Events.Include(e => e.Creator).AsNoTracking();
        }

        public Event? GetOneEvent(int eventId, bool trackChanges)
        {
            return FindByCondition(e => e.EventId.Equals(eventId), trackChanges);
        }
    }
}