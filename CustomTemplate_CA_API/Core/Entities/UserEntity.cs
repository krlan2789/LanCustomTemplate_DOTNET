using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Core.Entities;

[Index(nameof(Email), IsUnique = true)]
public class UserEntity
{
    [Key]
    public int Id { get; set; }

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