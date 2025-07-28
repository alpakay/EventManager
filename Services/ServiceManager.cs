using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IEventService _eventService;

        public ServiceManager(IEventService eventService)
        {
            _eventService = eventService;
        }
        public IEventService EventService => _eventService;
    }
}