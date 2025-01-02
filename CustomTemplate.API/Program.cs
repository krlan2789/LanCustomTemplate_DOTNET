
using System.Text;
using CustomTemplate.API.Data;
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

        // Add Database Context services to the container
        builder.Services
            .AddDbContext<CustomTemplateDatabaseContext>(option =>
            {
                option.UseSqlServer("" + builder.Configuration.GetConnectionString("DefaultConnection"));
            });

        // Add configuration to the container
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        // Add services to the container.
        string tokenIssuer = "" + builder.Configuration["Jwt:Issuer"];
        string tokenAudience = "" + builder.Configuration["Jwt:Audience"];
        string secretKey = "" + builder.Configuration["Jwt:SecretKey"];
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenIssuer,
                    ValidAudience = tokenAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        builder.Services.AddSingleton(new TokenService(secretKey, tokenIssuer, tokenAudience));
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
                    .WithTitle("CustomTemplate - REST API");
            });

            await app.MigrateDbAsync();

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
        app.UseSession();
        app.UseMiddleware<UserSessionLoggingMiddleware>();
        app.MapControllers();

        app.Run();
    }
}
