using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Core.Entities;

public class UserSessionLogEntity
{
    [Key]
    public int Id { get; set; }

    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Action { get; set; }

    [Required]
    public required int? UserId { get; set; }

    [Required]
    public required UserEntity? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public UserSessionLogEntity()
    {
        CreatedAt = DateTime.Now;
    }
}