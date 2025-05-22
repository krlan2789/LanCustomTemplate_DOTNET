using CustomTemplate_CA_API.Core.Entities;
using CustomTemplate_CA_API.Infrastructure.Persistence;

namespace CustomTemplate_CA_API.Infrastructure.Seeders;

public class UserProfileSeeder
{
    public static async Task Seed(AppDatabaseContext context)
    {
        if (!context.UserProfiles.Any())
        {
            var profiles = Enumerable.Range(1, 96).Select(x =>
                new UserProfileEntity
                {
                    UserId = x,
                    PhoneNumber = Faker.Phone.Number(),
                    Bio = Faker.Lorem.Sentence(Faker.RandomNumber.Next(2, 16)),
                }
            ).ToList();
            context.UserProfiles.AddRange(profiles);
            await context.SaveChangesAsync();
        }
    }
}
