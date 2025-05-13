using System.Security.Claims;
using CustomTemplate.API.Entities;

namespace CustomTemplate.API.Interfaces;

public interface ITokenService
{
    string GenerateToken(string username, TimeSpan expiration);
    ClaimsPrincipal GetPrincipalFromToken(string token);
    string? GetUsername(HttpContext httpContext);
    Task<User?> GetUser(HttpContext httpContext);
}