using CustomTemplate_CA_API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_API.Infrastructure.Seeders;

public static class DatabaseSeeder
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var context = new AppDatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<AppDatabaseContext>>());
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.EnsureDeleted();
            await context.Database.MigrateAsync();
        }
        await UserSeeder.Seed(context);
        await UserProfileSeeder.Seed(context);
    }
}
