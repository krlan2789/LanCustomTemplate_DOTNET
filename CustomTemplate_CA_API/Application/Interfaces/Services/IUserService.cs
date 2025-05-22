using CustomTemplate_CA_API.Application.Dtos;

namespace CustomTemplate_CA_API.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<UserProfileDto?> GetProfile(string username);
        public Task<UserProfileDto?> UpdateProfile(string username, UpdateUserProfileDto dto);
    }
}
