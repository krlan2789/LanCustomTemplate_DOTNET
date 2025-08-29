using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_Module.Domain.Entities;

[Table("CustomTemplate_CA_s"), Index(nameof(Email), IsUnique = true), Index(nameof(PhoneNumber), IsUnique = true)]
public class CustomTemplate_CA_Entity : BaseSoftDeletableEntity
{
	[Required, MaxLength(255)]
	public required string Name { get; set; }

	[Required, MaxLength(128)]
	public required string Email { get; set; }

	[Required, MaxLength(32)]
	public required string PhoneNumber { get; set; }

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