using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomTemplate_CA_Module.Domain.Entities;

[Table("Users"), Index(nameof(Email), IsUnique = true), Index(nameof(PhoneNumber), IsUnique = true)]
public class CustomTemplate_CA_Entity
{
	[Key]
	public string Id { get; set; } = Guid.NewGuid().ToString();

	[Required, MaxLength(255)]
    public required string Name { get; set; }

    [Required, MaxLength(128)]
    public required string Email { get; set; }

    [Required, MaxLength(32)]
    public required string PhoneNumber { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public DateTime? DeletedAt { get; set; }

	public CustomTemplate_CA_Entity SetName(string? name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		return this;
	}

	public CustomTemplate_CA_Entity SetEmail(string? email)
	{
		if (!string.IsNullOrWhiteSpace(email)) Email = email;
		return this;
	}

	public CustomTemplate_CA_Entity SetPhoneNumber(string? phoneNumber)
	{
		if (!string.IsNullOrWhiteSpace(phoneNumber)) PhoneNumber = phoneNumber;
		return this;
	}
}