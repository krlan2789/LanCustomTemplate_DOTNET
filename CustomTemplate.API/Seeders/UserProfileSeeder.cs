using CustomTemplate.API.Data;
using CustomTemplate.API.Entities;

namespace CustomTemplate.API.Seeders;

public class UserProfileSeeder
{
    public static async Task Seed(LanDatabaseContext context)
    {
        if (!context.UserProfiles.Any())
        {
            var profiles = Enumerable.Range(1, 96).Select(x =>
                new UserProfile
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
