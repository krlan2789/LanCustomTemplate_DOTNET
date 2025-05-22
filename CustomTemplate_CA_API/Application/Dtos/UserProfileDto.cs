namespace CustomTemplate_CA_API.Application.Dtos;

public record class UserProfileDto
(
    string Fullname,
    string? Username,
    string? Bio,
    string? ProfileImage,
    string? PhoneNumber,
    DateTime CreatedAt
);