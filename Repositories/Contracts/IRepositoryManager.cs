namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }
        IUserRepository User { get; }
        IKeyRepository Key { get; }

        void Save();
    }
}