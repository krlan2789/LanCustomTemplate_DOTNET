using CustomTemplate_CA_API.Core.Helper;
using CustomTemplate_CA_API.Infrastructure.Persistence;
using CustomTemplate_CA_API.Core.Entities;

namespace CustomTemplate_CA_API.Infrastructure.Seeders;

public static class UserSeeder
{
    public static async Task Seed(AppDatabaseContext context)
    {
        if (!context.Users.Any())
        {
            var users = Enumerable.Range(1, 128).Select(x =>
                new UserEntity
                {
                    Fullname = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    Username = Faker.Internet.UserName(),
                    PasswordHash = "12345678".Hash()
                }
            ).ToList();
            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}
