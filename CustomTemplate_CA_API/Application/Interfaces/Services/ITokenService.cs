using CustomTemplate_CA_API.Application.Dtos;
using System.Security.Claims;

namespace CustomTemplate_CA_API.Application.Interfaces.Services;

public interface ITokenService
{
    public string GenerateToken(string username, TimeSpan expiration);
    public ClaimsPrincipal GetPrincipalFromToken(string token);
    public string? GetUsername(HttpContext httpContext);
    public Task<UserDto?> GetUser(HttpContext httpContext);
}