using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext context) : base(context)
        {

        }

        public IQueryable<Event> GetAllEvents(bool trackChanges) => GetAll(trackChanges);

        public Event? GetOneEvent(int eventId, bool trackChanges)
        {
            return FindByCondition(e => e.EventId.Equals(eventId), trackChanges);
        }
    }
}