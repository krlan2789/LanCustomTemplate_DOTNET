using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomTemplate_CA_API.Core.Entities;

[Table("Users"), Index(nameof(Email), IsUnique = true), Index(nameof(Username), IsUnique = true)]
public class UserEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required, MaxLength(255)]
    public required string Fullname { get; set; }

    [Required, MaxLength(64)]
    public required string Username { get; set; }

    [Required, MaxLength(128)]
    public required string Email { get; set; }

    [Required, MaxLength(255)]
    public required string PasswordHash { get; set; }

    public UserProfileEntity? Profile { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public UserEntity()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}