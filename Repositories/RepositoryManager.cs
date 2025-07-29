namespace Repositories.Contracts
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public RepositoryManager(RepositoryContext context, IEventRepository eventRepository, IUserRepository userRepository)
        {
            _context = context;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public IEventRepository Event => _eventRepository;
        public IUserRepository User => _userRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}