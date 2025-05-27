using CustomTemplate_CA_API.Core.Entities;
using CustomTemplate_CA_API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_API.Infrastructure.Seeders;

public class UserProfileSeeder
{
    public static async Task Seed(AppDatabaseContext context)
    {
        if (!context.UserProfiles.Any())
        {
            var profiles = context.UserProfiles
                .Include(p => p.User)
                .Take(96)
                .Select(e =>
                    new UserProfileEntity
                    {
                        UserId = e.Id,
                        PhoneNumber = Faker.Phone.Number(),
                        Bio = Faker.Lorem.Sentence(Faker.RandomNumber.Next(2, 16)),
                    }
                ).ToList();
            context.UserProfiles.AddRange(profiles);
            await context.SaveChangesAsync();
        }
    }
}
