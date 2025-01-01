using System.ComponentModel.DataAnnotations;

namespace CustomTemplate.API.Entities;

public class UserSessionLog
{
    [Key]
    public int Id { get; set; }

    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Action { get; set; }

    [Required]
    public int? UserId { get; set; }
    [Required]
    public User? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public UserSessionLog()
    {
        CreatedAt = DateTime.Now;
    }
}
