using Entities.Models;
using Repositories.Contracts;

namespace Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateUser(User userEntity) => Create(userEntity);

    public IQueryable<User> GetAllUsers(bool trackChanges) => GetAll(trackChanges);

    public User? GetOneUser(int userId, bool trackChanges)
    {
        return FindByCondition(u => u.UserId.Equals(userId), trackChanges);
    }

    public User? GetUserByEmail(string email, bool trackChanges)
    {
        return FindByCondition(u => u.Email.Equals(email), trackChanges);
    }
}