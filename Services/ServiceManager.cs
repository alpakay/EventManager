using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public ServiceManager(IEventService eventService, IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _eventService = eventService;
            _authService = authService;
        }
        public IEventService EventService => _eventService;
        public IUserService UserService => _userService;
        public IAuthService AuthService => _authService;
    }
}