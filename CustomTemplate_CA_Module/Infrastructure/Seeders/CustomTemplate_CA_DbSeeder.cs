using Bogus;
using CustomTemplate_CA_Module.Domain.Entities;
using CustomTemplate_CA_Module.Infrastructure.Persistence;
using CustomTemplate_CA_Module.Infrastructure.Seeders.Fakes;

namespace CustomTemplate_CA_Module.Infrastructure.Seeders;

public class CustomTemplate_CA_DbSeeder(CustomTemplate_CA_DatabaseContext dbContext)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (!dbContext.CustomTemplate_CA_s.Any())
        {
            // Create Users
			var faker = new Faker();
			var users = Enumerable.Range(0, 32).Select(x =>
            {
                return new CustomTemplate_CA_Entity
				{
                    Name = faker.Name.FullName(),
                    Email = faker.Internet.Email(),
					PhoneNumber = faker.Phone.PhoneNumber("+62###########"),
                };
            }).ToList();

            CustomTemplate_CA_Fake userFake = new();
            users.AddRange(userFake.Generate(120));
            dbContext.CustomTemplate_CA_s.AddRange(users);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
