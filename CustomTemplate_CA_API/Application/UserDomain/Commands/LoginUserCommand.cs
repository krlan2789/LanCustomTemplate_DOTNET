using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Application.UserDomain.Commands;

public record class LoginUserCommand
(
    [Required, StringLength(128)] string Username,
    [Required, StringLength(128)] string Password
);