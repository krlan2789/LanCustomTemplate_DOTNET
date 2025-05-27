namespace CustomTemplate_CA_API.Application.SessionLogDomain.Dtos;

public record class SessionLogDto
(
    string Username,
    string IpAddress,
    string UserAgent,
    string Action,
    DateTime CreatedAt
);
