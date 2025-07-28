namespace Services.Contracts
{
    public interface IServiceManager
    {
        IEventService EventService { get; }
    }
}