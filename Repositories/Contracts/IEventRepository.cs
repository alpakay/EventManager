using Entities.Models;

namespace Repositories.Contracts
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        IQueryable<Event> GetAllEvents(bool trackChanges);
    }
}