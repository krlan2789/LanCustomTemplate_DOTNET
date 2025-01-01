namespace CustomTemplate.API.Dtos;

public record class ResponseUserDto
(
    string Fullname,
    string Email,
    string? Username,
    string? Bio,
    string? ProfileImage
);
