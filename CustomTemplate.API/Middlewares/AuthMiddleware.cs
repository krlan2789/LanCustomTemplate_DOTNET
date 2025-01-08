using CustomTemplate.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace CustomTemplate.API.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthMiddleware> _logger;

    public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, TokenService tokenService)
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
