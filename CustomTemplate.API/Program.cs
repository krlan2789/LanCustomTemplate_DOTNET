
using System.Text;
using CustomTemplate.API.Configurations;
using CustomTemplate.API.Data;
using CustomTemplate.API.Interfaces;
using CustomTemplate.API.Middlewares;
using CustomTemplate.API.Seeders;
using CustomTemplate.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace CustomTemplate.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add configuration to the container
        builder.Services.Configure<JwtTokenSettings>(builder.Configuration.GetSection("Jwt"));

        // Add Database Context services to the container
        builder.Services
            .AddDbContext<LanDatabaseContext>(option =>
            {
                option.UseSqlServer("" + builder.Configuration.GetConnectionString("DefaultConnection"));
            });

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
        builder.Services.AddScoped<ITokenService, JwtTokenService>();
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
                    .WithTitle((builder.Configuration["AppName"] ?? "CustomTemplate") + " - REST API");
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
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<AuthMiddleware>();
        app.UseMiddleware<UserSessionLoggingMiddleware>();
        app.MapControllers();

        app.Run();
    }
}
