namespace CustomTemplate_CA_API.Application.Dtos;

public record class CreateUserDto
(
    string Fullname,
    string Email,
    string Password,
    string? Username
);
