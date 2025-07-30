using System.Security.Claims;
using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(UserLoginDto userLoginDto);
    Task<ClaimsPrincipal> CreateUserPrincipalAsync(User user);
    Task LogoutAsync();
}

public class AuthResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public User? User { get; set; }
}