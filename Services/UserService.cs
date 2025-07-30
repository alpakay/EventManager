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

    public void CreateUser(UserRegisterDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        if (_repositoryManager.User.GetUserByEmail(user.Email, false) != null)
        {
            throw new InvalidOperationException("Email already exists.");
        }
        _repositoryManager.User.CreateUser(user);
        _repositoryManager.Save();
    }

    public int Login(UserLoginDto userLoginDto, bool trackChanges = false)
    {
        var user = _repositoryManager.User.GetUserByEmail(userLoginDto.Email, trackChanges);
        if (user == null || user.Password != userLoginDto.Password)
        {
            throw new UnauthorizedAccessException("Invalid login attempt");
        }
        return user.UserId;
        // Additional logic for successful login can be added here
    }
}