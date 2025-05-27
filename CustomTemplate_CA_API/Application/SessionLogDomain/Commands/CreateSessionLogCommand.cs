using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Application.SessionLogDomain.Commands;

public record class CreateSessionLogCommand
(
    [Required, StringLength(128)] string Username,
    [Required, StringLength(45)] string IpAddress,
    [Required, StringLength(256)] string UserAgent,
    [Required, StringLength(256)] string Action
);
