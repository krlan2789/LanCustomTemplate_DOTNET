using CustomTemplate_CA_API.Application.CredentialDomain.Services;
using Microsoft.AspNetCore.Authorization;

namespace CustomTemplate_CA_API.Presentation.Middlewares;

public class AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<AuthMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, JwtTokenService tokenService)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            // Only available for endpoints that has [Authorize]
            var hasAuthorize = endpoint.Metadata.OfType<AuthorizeAttribute>().Any();
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            _logger.LogInformation("AuthMiddleware: Endpoint: {Endpoint}. User-Agent: {UserAgent}", endpoint, userAgent);
            if (hasAuthorize)
            {
                _logger.LogInformation("AuthMiddleware: Endpoint requires authorization.");
                var currentUser = await tokenService.GetUser(context);
                if (currentUser == null)
                {
                    _logger.LogWarning("AuthMiddleware: Unauthorized user.");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    var message = "Unauthorized user.";
                    await context.Response.WriteAsync(message);
                    return;
                }
                _logger.LogInformation("AuthMiddleware: Authorized user.");
            }
        }
        await _next(context);
    }
}
