using CustomTemplate.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate.API.Seeders;

public static class DatabaseSeeder
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var context = new CustomTemplateDatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<CustomTemplateDatabaseContext>>());
        if (context.Database.GetPendingMigrations().Count() > 0)
        {
            context.Database.EnsureDeleted();
            await context.Database.MigrateAsync();
        }
        await UserSeeder.Seed(context);
        await UserProfileSeeder.Seed(context);
    }
}
