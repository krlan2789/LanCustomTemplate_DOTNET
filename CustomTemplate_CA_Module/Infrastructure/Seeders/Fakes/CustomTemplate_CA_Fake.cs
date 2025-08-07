using Bogus;
using CustomTemplate_CA_Module.Domain.Entities;

namespace CustomTemplate_CA_Module.Infrastructure.Seeders.Fakes;

public class CustomTemplate_CA_Fake : Faker<CustomTemplate_CA_Entity>
{
    public CustomTemplate_CA_Fake()
    {
        RuleFor(u => u.Name, f => f.Name.FullName());
        RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber());
        RuleFor(u => u.Email, f => f.Internet.Email());
        RuleFor(u => u.CreatedAt, f => f.Date.Past(1));
    }
}
