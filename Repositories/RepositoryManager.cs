namespace Repositories.Contracts
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IKeyRepository _keyRepository;

        public RepositoryManager(RepositoryContext context, IEventRepository eventRepository, IUserRepository userRepository, IKeyRepository keyRepository)
        {
            _context = context;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _keyRepository = keyRepository;
        }

        public IEventRepository Event => _eventRepository;
        public IUserRepository User => _userRepository;
        public IKeyRepository Key => _keyRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}