using System.Security.Claims;
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

    public AuthService(IRepositoryManager repositoryManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
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

        if (userLoginDto.Password != user.Password)
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
}