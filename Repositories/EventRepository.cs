using System.Linq.Expressions;
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

        public IQueryable<Event> GetEventsByCondition(Expression<Func<Event, bool>> expression, bool trackChanges)
        {
            return FindByConditionQueryable(expression, trackChanges).Include(e => e.Creator);
        }

        public Event? GetOneEvent(int eventId, bool trackChanges)
        {
            return FindByConditionQueryable(e => e.EventId == eventId, trackChanges)
                .Include(e => e.Creator)
                .SingleOrDefault();
        }
    }
}