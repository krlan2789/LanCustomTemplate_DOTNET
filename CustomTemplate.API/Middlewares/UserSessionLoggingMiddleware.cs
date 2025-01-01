using System;
using CustomTemplate.API.Data;
using CustomTemplate.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate.API.Middlewares;

public class UserSessionLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public UserSessionLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, CustomTemplateDatabaseContext dbContext)
    {
        string ipAddress = "" + context.Connection.RemoteIpAddress?.ToString();
        string userAgent = "" + context.Request.Headers["User-Agent"].FirstOrDefault("Unknown");
        var username = context.User?.Identity?.IsAuthenticated == true ? context.User.Identity.Name : null;
        User? currentUser = await dbContext.Users.Where(user => user.Email == username).FirstOrDefaultAsync();

        var userSessionLog = new UserSessionLog
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
