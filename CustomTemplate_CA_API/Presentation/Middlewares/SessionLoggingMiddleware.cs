using CustomTemplate_CA_API.Application.SessionLogDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Interfaces;
using CustomTemplate_CA_API.Core.Entities;

namespace CustomTemplate_CA_API.Presentation.Middlewares;

public class SessionLoggingMiddleware(RequestDelegate next, ILogger<SessionLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<SessionLoggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var sessionLogRepository = scope.ServiceProvider.GetRequiredService<ISessionLogRepository>();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var username = context.User?.Identity?.IsAuthenticated == true ? context.User.Identity.Name : null;
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var requestPath = "" + context.Request.Method + " " + context.Request.Path;

        if (!string.IsNullOrEmpty(username))
        {
            var sessionLog = new SessionLogEntity
            {
                IpAddress = ipAddress,
                UserAgent = context.Request.Headers.UserAgent.ToString(),
                Action = requestPath,
                CreatedAt = DateTime.UtcNow,
            };
            var user = await userRepository.FindByUsernameAsync(username);
            if (user != null)
            {
                sessionLog.UserId = user.Id;
            }

            await sessionLogRepository.AddAsync(sessionLog);
            _logger.LogInformation("Session log recorded for user {Username} at {RequestPath}", username, requestPath);
        }

        await _next(context);
    }
}