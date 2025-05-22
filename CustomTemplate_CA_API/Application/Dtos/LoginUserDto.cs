using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Application.Dtos;

public record class LoginUserDto
(
    [Required, StringLength(128)] string Username,
    [Required, StringLength(64)] string Password
);