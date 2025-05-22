using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Application.Dtos;

public record class RegisterUserDto
(
    [Required, StringLength(255)] string Fullname,
    [Required, EmailAddress, StringLength(128)] string Email,
    [Required, StringLength(64)] string Password,
    [Required, StringLength(64)] string PasswordVerify
);
