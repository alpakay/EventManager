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
    }
}