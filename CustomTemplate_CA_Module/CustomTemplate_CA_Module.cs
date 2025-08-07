using CustomTemplate_CA_Module.Application.Abstractions;
using CustomTemplate_CA_Module.Application.Factories;
using CustomTemplate_CA_Module.Infrastructure.Persistence;
using CustomTemplate_CA_Module.Infrastructure.Persistence.Repositories;
using CustomTemplate_CA_Module.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomTemplate_CA_Module;

public static class CustomTemplate_CA_Module
{
	public static IServiceCollection AddCustomTemplate_CA_Module(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");
		services.AddDbContextPool<CustomTemplate_CA_DatabaseContext>(options =>
		{
			options.UseSqlite(connectionString?.Replace("DATABASE_NAME", nameof(CustomTemplate_CA_Module)));
		}, 256);

		services.AddScoped<ICustomTemplate_CA_ReadRepository, CustomTemplate_CA_ReadRepository>();
		services.AddScoped<ICustomTemplate_CA_WriteRepository, CustomTemplate_CA_WriteRepository>();
		services.AddScoped<CustomTemplate_CA_DbSeeder>();
		services.AddSingleton<CustomTemplate_CA_HandlerFactory>();
		services.Scan(scan => scan
			.FromAssemblies(typeof(CustomTemplate_CA_HandlerFactory).Assembly)
			.AddClasses(classes => classes.Where(type => type.Name.EndsWith("CommandHandler") || type.Name.EndsWith("QueryHandler")))
			.AsSelf()
			.WithScopedLifetime());

		return services;
	}
}
