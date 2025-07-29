using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;

        public ServiceManager(IEventService eventService, IUserService userService)
        {
            _userService = userService;
            _eventService = eventService;
        }
        public IEventService EventService => _eventService;
        public IUserService UserService => _userService;
    }
}