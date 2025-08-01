using Entities.Models;

namespace Repositories.Contracts;

public interface IKeyRepository : IRepositoryBase<Key>
{
    string GetKeyById(int keyId, bool trackChanges);
}