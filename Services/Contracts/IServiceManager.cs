namespace Services.Contracts
{
    public interface IServiceManager
    {
        IEventService EventService { get; }
        IUserService UserService { get; }
        IAuthService AuthService { get; }
        IEncryptionService EncryptionService { get; }
        IFileService FileService { get; }
    }
}