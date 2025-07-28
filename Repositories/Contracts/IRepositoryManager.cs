namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }

        void Save();
    }
}