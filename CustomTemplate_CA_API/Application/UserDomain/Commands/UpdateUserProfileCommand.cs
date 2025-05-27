using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Application.UserDomain.Commands;

public record class UpdateUserProfileCommand
(
    [Required, MinLength(4), MaxLength(128)] string Username,
    [MaxLength(512)] string? Fullname,
    string? Bio,
    [MinLength(4)] string? ProfileImage,
    [MinLength(8), MaxLength(32)] string? PhoneNumber
);
