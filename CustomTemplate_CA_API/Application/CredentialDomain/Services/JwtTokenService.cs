using CustomTemplate_CA_API.Application.CredentialDomain.Configurations;
using CustomTemplate_CA_API.Application.CredentialDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Dtos;
using CustomTemplate_CA_API.Application.UserDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Mapping;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomTemplate_CA_API.Application.CredentialDomain.Services;

public sealed class JwtTokenService(IOptions<JwtTokenSettings> options, ILogger<ITokenService> logger, IUserRepository userRepository) : ITokenService
{
    private readonly JwtTokenSettings _options = options.Value;
    private readonly ILogger<ITokenService> _logger = logger;
    private readonly IUserRepository _userRepository = userRepository;

    public string GenerateToken(string username, TimeSpan expiration)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(expiration),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            ValidateIssuer = true,
            ValidIssuer = _options.Issuer,
            ValidateAudience = true,
            ValidAudience = _options.Audience,
            ValidateLifetime = true
        };
        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }

    public string? GetUsername(HttpContext httpContext)
    {
        try
        {
            string token = "" + httpContext.Request.Headers.Authorization.ToString().Split(" ")[1];
            _logger.LogDebug("JwtTokenService: Token={Token}", token);
            var username = GetPrincipalFromToken(token).Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            _logger.LogDebug("JwtTokenService: Username={Username}", username);
            return username ?? null;
        }
        catch (SecurityTokenException e)
        {
            _logger.LogError(e, "JwtTokenService: Error getting username from token: {Message}", e.Message);
            return null;
        }
    }

    public async Task<UserDto?> GetUser(HttpContext httpContext)
    {
        var username = "" + GetUsername(httpContext);
        var currentUser = await _userRepository.FindByUsernameAsync(username);
        return currentUser?.ToDto();
    }
}
