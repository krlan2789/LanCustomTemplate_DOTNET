using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomTemplate_CA_API.Core.Entities;

[Table("UserProfiles"), Index(nameof(PhoneNumber), IsUnique = true)]
public class UserProfileEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Bio { get; set; }

    public string? ProfileImage { get; set; }

    [MaxLength(32)]
    public string? PhoneNumber { get; set; }

    [Required]
    public required string UserId { get; set; }

    public UserEntity? User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public UserProfileEntity()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}