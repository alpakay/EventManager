namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }
        IUserRepository User { get; }

        void Save();
    }
}