using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class AuthService : IAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEncryptionService _encryptionService;

    public AuthService(IRepositoryManager repositoryManager, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEncryptionService encryptionService)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _encryptionService = encryptionService;
    }

    public async Task<ClaimsPrincipal> CreateUserPrincipalAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
        return new ClaimsPrincipal(claimsIdentity);
    }

    public async Task<AuthResult> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = _repositoryManager.User.GetUserByEmail(userLoginDto.Email, false);
        if (user == null)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "Kayıtlı kullanıcı bulunamadı."
            };
        }

        var decryptedPassword = _encryptionService.Decrypt(user.Password);

        if (userLoginDto.Password != decryptedPassword)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "Geçersiz email veya şifre."
            };
        }

        var principal = await CreateUserPrincipalAsync(user);
        await _httpContextAccessor.HttpContext.SignInAsync("CookieAuth", principal);

        return new AuthResult
        {
            Success = true,
            User = user
        };
    }

    public async Task LogoutAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync("CookieAuth");
    }

    public async Task<bool> RegisterAsync(UserProfileDto userDto)
    {
        if (_repositoryManager.User.GetUserByEmail(userDto.Email, false) != null)
        {
            throw new InvalidOperationException("Bu email zaten kayıtlı.");
        }
        var cryptedPassword = _encryptionService.Encrypt(userDto.Password);

        var user = _mapper.Map<User>(userDto);
        user.Password = cryptedPassword;

        _repositoryManager.User.CreateUser(user);
        _repositoryManager.Save();
        return true;
    }
}