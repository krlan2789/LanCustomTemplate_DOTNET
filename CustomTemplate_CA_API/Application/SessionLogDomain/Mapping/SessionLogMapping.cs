using CustomTemplate_CA_API.Application.SessionLogDomain.Commands;
using CustomTemplate_CA_API.Application.SessionLogDomain.Dtos;
using CustomTemplate_CA_API.Core.Entities;

namespace CustomTemplate_CA_API.Application.SessionLogDomain.Mapping;

public static class SessionLogMapping
{
    public static SessionLogEntity ToEntity(this CreateSessionLogCommand dto, UserEntity user)
    {
        return new SessionLogEntity
        {
            UserId = user.Id,
            IpAddress = dto.IpAddress,
            UserAgent = dto.UserAgent,
            CreatedAt = DateTime.Now
        };
    }
    public static SessionLogDto ToDto(this SessionLogEntity entity)
    {
        return new SessionLogDto
        (
            entity.User!.Username,
            entity.IpAddress ?? "Unknown IP",
            entity.UserAgent ?? "Unknown User Agent",
            entity.Action ?? "Unknown Action",
            entity.CreatedAt
        );
    }
}
