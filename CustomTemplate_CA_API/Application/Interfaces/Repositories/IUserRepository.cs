using CustomTemplate_CA_API.Core.Entities;

namespace CustomTemplate_CA_API.Application.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<UserEntity?> FindByUsernameAsync(string username);
    public Task<UserEntity?> FindSecureAsync(string username, string password);
    public Task<UserEntity?> FindByEmailAsync(string email);
    public Task<string?> AddAsync(UserEntity entity);
    public Task<string?> DeleteByUsernameAsync(string username);
    public Task<bool> ExistsByUsernameAsync(string username);
    public Task<bool> ExistsByEmailAsync(string email);
    public Task<UserProfileEntity?> GetProfileAsync(string username);
    public Task<string?> UpdateProfileAsync(string username, UserProfileEntity entity);
}
