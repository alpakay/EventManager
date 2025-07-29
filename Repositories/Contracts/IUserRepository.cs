using Entities.Models;

namespace Repositories.Contracts;

public interface IUserRepository : IRepositoryBase<User>
{
    IQueryable<User> GetAllUsers(bool trackChanges);
    User? GetOneUser(int userId, bool trackChanges);
    void CreateUser(User userEntity);
    User? GetUserByEmail(string email, bool trackChanges);
}