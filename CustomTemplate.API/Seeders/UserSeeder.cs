using CustomTemplate.API.Data;
using CustomTemplate.API.Entities;
using CustomTemplate.API.Helper;

namespace CustomTemplate.API.Seeders;

public static class UserSeeder
{
    public static async Task Seed(LanDatabaseContext context)
    {
        if (!context.Users.Any())
        {
            var users = Enumerable.Range(1, 128).Select(x =>
                new User
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
