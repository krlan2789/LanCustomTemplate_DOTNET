using System.Text.RegularExpressions;
using CustomTemplate.API.Dtos;
using CustomTemplate.API.Entities;
using CustomTemplate.API.Helper;

namespace CustomTemplate.API.Mapping;

public static class UserMapping
{
    public static User ToEntity(this RegisterUserDto dto)
    {
        return new User
        {
            Fullname = dto.Fullname,
            Email = dto.Email,
            Username = Regex.Replace("" + dto.Email.Split('@')[0], @"[^a-zA-Z0-9._]", string.Empty),
            PasswordHash = dto.Password.Hash(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static ResponseUserDto ToResponseDto(this User user)
    {
        return new ResponseUserDto
        (
            user.Fullname,
            user.Email,
            user.Username,
            user.Profile?.Bio,
            user.Profile?.ProfileImage
        );
    }

    public static bool VerifyPassword(this User user, string password)
    {
        return password.VerifyHashed(user.PasswordHash);
    }
}
