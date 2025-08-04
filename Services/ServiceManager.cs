using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IEncryptionService _encryptionService;
        private readonly IFileService _fileService;

        public ServiceManager(IEventService eventService, IUserService userService, IAuthService authService, IEncryptionService encryptionService, IFileService fileService)
        {
            _eventService = eventService;
            _userService = userService;
            _authService = authService;
            _encryptionService = encryptionService;
            _fileService = fileService;
        }
        public IEventService EventService => _eventService;
        public IUserService UserService => _userService;
        public IAuthService AuthService => _authService;
        public IEncryptionService EncryptionService => _encryptionService;
        public IFileService FileService => _fileService;
    }
}