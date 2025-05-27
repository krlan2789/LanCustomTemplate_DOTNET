using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Application.UserDomain.Commands;

public record class RegisterUserCommand
(
    [Required, StringLength(250)] string Fullname,
    [Required, EmailAddress, StringLength(250)] string Email,
    [Required, StringLength(128)] string Password,
    [Required, StringLength(128)] string PasswordVerify
);
