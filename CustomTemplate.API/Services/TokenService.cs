using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CustomTemplate.API.Data;
using CustomTemplate.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CustomTemplate.API.Services;

public class TokenService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly ILogger<TokenService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private CustomTemplateDatabaseContext DbContext => _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<CustomTemplateDatabaseContext>();

    public TokenService(IConfiguration configuration, ILogger<TokenService> logger, IServiceProvider serviceProvider)
    {
        _secretKey = "" + configuration["Jwt:SecretKey"];
        _issuer = "" + configuration["Jwt:Issuer"];
        _audience = "" + configuration["Jwt:Audience"];
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public string GenerateToken(string username, TimeSpan expiration)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.Add(expiration),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey)),
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidAudience = _audience,
            ValidateLifetime = true
        };
        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }

    public string? GetUsername(HttpContext httpContext)
    {
        try
        {
            string token = "" + httpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            _logger.LogInformation("TokenService: Token={Token}", token);
            var username = GetPrincipalFromToken(token).Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            _logger.LogInformation("TokenService: Username={Username}", username);
            return username ?? null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<User?> GetUser(HttpContext httpContext)
    {
        var username = GetUsername(httpContext);
        User? currentUser = await DbContext.Users.Where(user => user.Username == username).FirstOrDefaultAsync();
        return currentUser;
    }
}
