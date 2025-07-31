using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public UserService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public IQueryable<User> GetAllUsers(bool trackChanges)
    {
        return _repositoryManager.User.GetAllUsers(trackChanges);
    }

    public User? GetOneUser(int userId, bool trackChanges)
    {
        return _repositoryManager.User.GetOneUser(userId, trackChanges);
    }

    public void CreateUser(UserProfileDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        if (_repositoryManager.User.GetUserByEmail(user.Email, false) != null)
        {
            throw new InvalidOperationException("Bu email zaten kayıtlı.");
        }
        _repositoryManager.User.CreateUser(user);
        _repositoryManager.Save();
    }

    public void UpdateUser(int userId, UserProfileDto userEntity)
    {
        var user = _repositoryManager.User.GetOneUser(userId, true);
        if (user == null)
        {
            throw new InvalidOperationException("Kullanıcı bulunamadı.");
        }
        if (_repositoryManager.User.GetUserByEmail(userEntity.Email, false) != null)
        {
            throw new InvalidOperationException("Bu email zaten kayıtlı.");
        }
        user.FullName = userEntity.FullName;
        user.Email = userEntity.Email;
        user.BirthDate = userEntity.BirthDate!.Value;
        user.Password = userEntity.Password;
        
        _repositoryManager.User.UpdateUser(user);
        _repositoryManager.Save();
    }

    public void DeleteUser(int userId)
    {
        var user = _repositoryManager.User.GetOneUser(userId, false);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }
        _repositoryManager.User.DeleteUser(user);
        _repositoryManager.Save();
    }
}