using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts
{
    public interface IUserService
    {
        IQueryable<User> GetAllUsers(bool trackChanges);
        User? GetOneUser(int userId, bool trackChanges);
        void CreateUser(UserRegisterDto userEntity);
        int Login(UserLoginDto userLoginDto, bool trackChanges);
    }
}