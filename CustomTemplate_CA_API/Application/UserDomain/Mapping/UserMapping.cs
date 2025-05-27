using CustomTemplate_CA_API.Application.UserDomain.Commands;
using CustomTemplate_CA_API.Application.UserDomain.Dtos;
using CustomTemplate_CA_API.Core.Entities;
using CustomTemplate_CA_API.Core.Helper;
using System.Text.RegularExpressions;

namespace CustomTemplate_CA_API.Application.UserDomain.Mapping;

public static class UserMapping
{
    public static UserEntity ToEntity(this RegisterUserCommand dto)
    {
        return new UserEntity
        {
            Fullname = dto.Fullname,
            Email = dto.Email,
            Username = Regex.Replace("" + dto.Email.Split('@')[0], @"[^a-zA-Z0-9._]", ""),
            PasswordHash = dto.Password.Hash(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static UserDto ToDto(this UserEntity entity)
    {
        return new UserDto
        (
            entity.Fullname,
            entity.Email,
            entity.Username,
            entity.Profile?.Bio,
            entity.Profile?.ProfileImage,
            entity.CreatedAt
        );
    }
}
