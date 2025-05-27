using CustomTemplate_CA_API.Application.UserDomain.Commands;
using CustomTemplate_CA_API.Application.UserDomain.Dtos;
using CustomTemplate_CA_API.Application.UserDomain.Queries;

namespace CustomTemplate_CA_API.Application.UserDomain.Interfaces;

public interface IUserService
{
    public Task<UserProfileDto?> GetProfile(UserProfileByUsernameQuery query);
    public Task<UserProfileDto?> UpdateProfile(UpdateUserProfileCommand command);
}
