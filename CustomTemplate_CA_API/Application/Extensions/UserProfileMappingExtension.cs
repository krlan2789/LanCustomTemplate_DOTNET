using CustomTemplate_CA_API.Application.Dtos;
using CustomTemplate_CA_API.Core.Entities;

namespace CustomTemplate_CA_API.Application.Extensions
{
    public static class UserProfileMappingExtension
    {
        public static UserProfileEntity ToEntity(this UpdateUserProfileDto dto, UserEntity user)
        {
            return new UserProfileEntity
            {
                UserId = user.Id,
                User = user,
                Bio = dto.Bio,
                ProfileImage = dto.ProfileImage,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static UserProfileDto ToDto(this UserProfileEntity entity)
        {
            return new UserProfileDto
            (
                entity.User!.Fullname,
                entity.User!.Username,
                entity.Bio,
                entity.ProfileImage,
                entity.PhoneNumber,
                entity.CreatedAt
            );
        }
    }
}
