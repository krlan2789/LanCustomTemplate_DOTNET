using System.ComponentModel.DataAnnotations;

namespace CustomTemplate.API.Dtos;

public record class LoginUserDto
(
    [Required, StringLength(128)] string Username,
    [Required, StringLength(64)] string Password
);