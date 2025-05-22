using CustomTemplate_CA_API.Core.Entities;
using CustomTemplate_CA_API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_API.Presentation.Middlewares;

public class UserSessionLoggingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, AppDatabaseContext dbContext)
    {
        string ipAddress = "" + context.Connection.RemoteIpAddress?.ToString();
        string userAgent = "" + context.Request.Headers["User-Agent"].FirstOrDefault("Unknown");
        var username = context.User?.Identity?.IsAuthenticated == true ? context.User.Identity.Name : null;
        UserEntity? currentUser = await dbContext.Users.Where(user => user.Email == username).FirstOrDefaultAsync();

        var userSessionLog = new UserSessionLogEntity
        {
            IpAddress = ipAddress,
            UserAgent = userAgent,
            UserId = currentUser?.Id,
        };

        dbContext.UserSessionLogs.Add(userSessionLog);
        await dbContext.SaveChangesAsync();

        await _next(context);
    }
}
