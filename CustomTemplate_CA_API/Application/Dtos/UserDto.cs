namespace CustomTemplate_CA_API.Application.Dtos;

public record class UserDto
(
    string Fullname,
    string Email,
    string? Username,
    string? Bio,
    string? ProfileImage,
    DateTime CreatedAt
);
