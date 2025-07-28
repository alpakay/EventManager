namespace Repositories.Contracts
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IEventRepository _eventRepository;

        public RepositoryManager(RepositoryContext context, IEventRepository eventRepository)
        {
            _context = context;
            _eventRepository = eventRepository;
        }

        public IEventRepository Event => _eventRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}