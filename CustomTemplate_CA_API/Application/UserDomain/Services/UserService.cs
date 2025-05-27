using CustomTemplate_CA_API.Application.UserDomain.Commands;
using CustomTemplate_CA_API.Application.UserDomain.Dtos;
using CustomTemplate_CA_API.Application.UserDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Mapping;
using CustomTemplate_CA_API.Application.UserDomain.Queries;
using CustomTemplate_CA_API.Core.Entities;

namespace CustomTemplate_CA_API.Application.UserDomain.Services;

public class UserService(ILogger<IUserService> logger, IUserRepository userRepository) : IUserService
{
    private readonly ILogger<IUserService> _logger = logger;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserProfileDto?> GetProfile(UserProfileByUsernameQuery query)
    {
        try
        {
            var userProfile = await _userRepository.GetProfileAsync(query.Username);
            return userProfile?.ToDto();
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user profile for {Username}", query.Username);
            return null;
        }
    }

    public async Task<UserProfileDto?> UpdateProfile(UpdateUserProfileCommand cmd)
    {
        try
        {
            if (await _userRepository.ExistsByUsernameAsync(cmd.Username))
            {
                _logger.LogWarning("User with username {Username} already exists", cmd.Username);
                return null;
            }
            UserEntity? currentUser = await _userRepository.FindByUsernameAsync(cmd.Username);
            if (currentUser == null)
            {
                _logger.LogWarning("User with username {Username} not found", cmd.Username);
                return null;
            }
            await _userRepository.UpdateProfileAsync(cmd.Username, cmd.ToEntity(currentUser));
            var userProfile = await _userRepository.GetProfileAsync(cmd.Username);
            return userProfile?.ToDto();
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user profile for {Username}", cmd.Username);
            return null;
        }
    }
}
