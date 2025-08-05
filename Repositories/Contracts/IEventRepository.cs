using System.Linq.Expressions;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        IQueryable<Event> GetAllEvents(bool trackChanges);
        Event? GetOneEvent(int eventId, bool trackChanges);
        IQueryable<Event> GetEventsByCondition(Expression<Func<Event, bool>> expression, bool trackChanges);

        void CreateEvent(Event eventEntity);

    }
}