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
    private readonly IEncryptionService _encryptionService;

    public UserService(IRepositoryManager repositoryManager, IMapper mapper, IEncryptionService encryptionService)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _encryptionService = encryptionService;
    }

    public IQueryable<User> GetAllUsers(bool trackChanges)
    {
        return _repositoryManager.User.GetAllUsers(trackChanges);
    }

    public User? GetOneUser(int userId, bool trackChanges)
    {
        return _repositoryManager.User.GetOneUser(userId, trackChanges);
    }

    public void UpdateUser(int userId, UserProfileDto userEntity)
    {
        var user = _repositoryManager.User.GetOneUser(userId, true);
        if (user == null)
        {
            throw new InvalidOperationException("Kullanıcı bulunamadı.");
        }
        if (_repositoryManager.User.GetUserByEmail(userEntity.Email, false) != null && 
            _repositoryManager.User.GetUserByEmail(userEntity.Email, false).UserId != userId)
        {
            throw new InvalidOperationException("Bu email zaten kayıtlı.");
        }

        var cryptedPassword = _encryptionService.Encrypt(userEntity.Password);

        user.FullName = userEntity.FullName;
        user.Email = userEntity.Email;
        user.BirthDate = userEntity.BirthDate;
        user.Password = cryptedPassword;
        
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