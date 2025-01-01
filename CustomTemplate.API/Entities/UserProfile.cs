using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate.API.Entities;

[Index(nameof(PhoneNumber), IsUnique = true)]
public class UserProfile
{
    [Key]
    public int Id { get; set; }

    public string? Bio { get; set; }
    public string? ProfileImage { get; set; }

    [MaxLength(32)]
    public string? PhoneNumber { get; set; }

    [Required]
    public int UserId { get; set; }
    [Required]
    public User? User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public UserProfile()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}
