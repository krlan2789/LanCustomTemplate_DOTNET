using CustomTemplate_CA_API.Application.CredentialDomain.Configurations;
using CustomTemplate_CA_API.Application.CredentialDomain.Interfaces;
using CustomTemplate_CA_API.Application.CredentialDomain.Services;
using CustomTemplate_CA_API.Application.SessionLogDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Services;
using CustomTemplate_CA_API.Core.Repositories;
using CustomTemplate_CA_API.Infrastructure.Persistence;
using CustomTemplate_CA_API.Infrastructure.Persistence.Repositories;
using CustomTemplate_CA_API.Infrastructure.Seeders;
using CustomTemplate_CA_API.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace CustomTemplate_CA_API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add configuration to the container
        builder.Services.Configure<JwtTokenSettings>(builder.Configuration.GetSection("Jwt"));

        // Add Database Context services to the container
        builder.Services
            .AddDbContextPool<AppDatabaseContext>(options =>
            {
                // Uncomment the database provider you want to use and remove the others
                options.UseSqlite("" + builder.Configuration.GetConnectionString("SqliteConnection"));
                //options.UseSqlServer("" + builder.Configuration.GetConnectionString("SqlServerConnection"));
                //options.UseNpgsql("" + builder.Configuration.GetConnectionString("PostgreSqlConnection"));
            }, 256);

        // Add services to the container
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtTokenSettings>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "" + jwtSettings?.Issuer,
                    ValidAudience = "" + jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("" + jwtSettings?.SecretKey))
                };
            });

        // Add session services to the container
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IUserRepository, IUserRepository>();
        builder.Services.AddScoped<ISessionLogRepository, SessionLogRepository>();
        builder.Services.AddScoped<ITokenService, JwtTokenService>();
        builder.Services.AddScoped<ICredentialService, CredentialService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Migrate the database
        if (app.Environment.IsDevelopment())
        {
            // Register the endpoint for viewing the OpenAPI Documentation
            string openApiRoute = "/api/docs/{documentName}/openapi.json";
            app.MapOpenApi(openApiRoute);
            app.MapScalarApiReference(options =>
            {
                string scalarApiRoute = "/api/docs/{documentName}";
                options
                    .WithTheme(ScalarTheme.BluePlanet)
                    .WithEndpointPrefix(scalarApiRoute)
                    .WithOpenApiRoutePattern(openApiRoute)
                    .WithTitle((builder.Configuration["AppName"] ?? "CustomTemplate_CA_API") + " - WEB API");
            });

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            await DatabaseSeeder.Seed(services);
        }

        // Configure the HTTP request pipeline.
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseRouting();
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<SessionLoggingMiddleware>();
        app.UseMiddleware<AuthMiddleware>();
        app.MapControllers();

        app.Run();
    }
}
