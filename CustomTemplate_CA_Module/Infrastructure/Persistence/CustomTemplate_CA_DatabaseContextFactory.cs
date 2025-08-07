using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CustomTemplate_CA_Module.Infrastructure.Persistence;

public class CustomTemplate_CA_DatabaseContextFactory : IDesignTimeDbContextFactory<CustomTemplate_CA_DatabaseContext>
{
	public CustomTemplate_CA_DatabaseContext CreateDbContext(string[] args)
	{
		// Adjust the path to point to the WebAPI project directory
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "WebAPI")))
			.AddJsonFile("appsettings.json", optional: false)
			.AddJsonFile("appsettings.Development.json", optional: true)
			.Build();

		var connectionString = configuration.GetConnectionString("DefaultConnection");
		var optionsBuilder = new DbContextOptionsBuilder<CustomTemplate_CA_DatabaseContext>();
		optionsBuilder.UseSqlite(connectionString?.Replace("DATABASE_NAME", nameof(CustomTemplate_CA_Module)));
		return new CustomTemplate_CA_DatabaseContext(optionsBuilder.Options);
	}
}
