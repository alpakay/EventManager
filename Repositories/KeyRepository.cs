using Entities.Models;
using Repositories.Contracts;

namespace Repositories;

public class KeyRepository : RepositoryBase<Key>, IKeyRepository
{
    public KeyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public string GetKeyById(int keyId, bool trackChanges)
    {
        return GetAll(trackChanges).FirstOrDefault(k => k.KeyId == keyId)?.KeyValue ?? string.Empty;
    }
}