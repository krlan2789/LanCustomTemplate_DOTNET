using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomTemplate_CA_API.Core.Entities;

[Table("SessionLogs")]
public class SessionLogEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }
    
    public string? Action { get; set; }

    public string? UserId { get; set; }

    public UserEntity? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public SessionLogEntity()
    {
        CreatedAt = DateTime.Now;
    }
}