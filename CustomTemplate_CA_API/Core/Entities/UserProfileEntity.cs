using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Core.Entities;

[Index(nameof(PhoneNumber), IsUnique = true)]
public class UserProfileEntity
{
    [Key]
    public int Id { get; set; }

    public string? Bio { get; set; }
    public string? ProfileImage { get; set; }

    [MaxLength(32)]
    public string? PhoneNumber { get; set; }

    [Required]
    public required int UserId { get; set; }

    [Required]
    public required UserEntity? User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public UserProfileEntity()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}