using CustomTemplate_CA_API.Application.Dtos;
using CustomTemplate_CA_API.Core.Entities;
using CustomTemplate_CA_API.Core.Helper;
using System.Text.RegularExpressions;

namespace CustomTemplate_CA_API.Application.Extensions;

public static class UserMappingExtensions
{
    public static UserEntity ToEntity(this RegisterUserDto dto)
    {
        return new UserEntity
        {
            Fullname = dto.Fullname,
            Email = dto.Email,
            Username = Regex.Replace("" + dto.Email.Split('@')[0], @"[^a-zA-Z0-9._]", string.Empty),
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

    public static bool VerifyPassword(this UserEntity entity, string password)
    {
        return password.VerifyHashed(entity.PasswordHash);
    }
}
