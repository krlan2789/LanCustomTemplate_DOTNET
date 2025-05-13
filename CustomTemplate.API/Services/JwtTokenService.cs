using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CustomTemplate.API.Configurations;
using CustomTemplate.API.Data;
using CustomTemplate.API.Entities;
using CustomTemplate.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CustomTemplate.API.Services;

public sealed class JwtTokenService : ITokenService
{
    private readonly JwtTokenSettings _options;
    private readonly ILogger<JwtTokenService> _logger;
    private readonly IDbContextFactory<LanDatabaseContext> _dbContextFactory;

    public JwtTokenService(IOptions<JwtTokenSettings> options, ILogger<JwtTokenService> logger, IDbContextFactory<LanDatabaseContext> dbContextFactory)
    {
        _options = options.Value;
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

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
            string token = "" + httpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            _logger.LogDebug("JwtTokenService: Token={Token}", token);
            var username = GetPrincipalFromToken(token).Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            _logger.LogDebug("JwtTokenService: Username={Username}", username);
            return username ?? null;
        }
        catch (SecurityTokenException e)
        {
            _logger.LogError($"JwtTokenService: Error getting username from token: {e.Message}");
            return null;
        }
    }

    public async Task<User?> GetUser(HttpContext httpContext)
    {
        var username = GetUsername(httpContext);
        using var dbContext = _dbContextFactory.CreateDbContext();
        User? currentUser = await dbContext.Users.Where(user => user.Username == username).FirstOrDefaultAsync();
        return currentUser;
    }
}
