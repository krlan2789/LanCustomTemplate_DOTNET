using CustomTemplate_CA_API.Application.Dtos;
using CustomTemplate_CA_API.Application.Extensions;
using CustomTemplate_CA_API.Application.Interfaces.Repositories;
using CustomTemplate_CA_API.Application.Interfaces.Services;
using CustomTemplate_CA_API.Core.Entities;

namespace CustomTemplate_CA_API.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserProfileDto?> GetProfile(string username)
        {
            var userProfile = await _userRepository.GetProfileAsync(username);
            return userProfile?.ToDto();
        }

        public async Task<UserProfileDto?> UpdateProfile(string username, UpdateUserProfileDto dto)
        {
            if (await _userRepository.ExistsByUsernameAsync(dto.Username)) return null;
            UserEntity? currentUser = await _userRepository.FindByUsernameAsync(username);
            if (currentUser == null) return null;
            string? result = await _userRepository.UpdateProfileAsync(username, dto.ToEntity(currentUser));
            var userProfile = await _userRepository.GetProfileAsync(username);
            return result != null && userProfile != null ? userProfile.ToDto() : null;
        }
    }
}
