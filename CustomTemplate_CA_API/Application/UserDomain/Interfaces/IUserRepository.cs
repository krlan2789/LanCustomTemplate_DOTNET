using CustomTemplate_CA_API.Core.Entities;
using CustomTemplate_CA_API.Core.Repositories;

namespace CustomTemplate_CA_API.Application.UserDomain.Interfaces;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    public Task<UserEntity?> FindByUsernameAsync(string username);
    public Task<UserEntity?> FindSecureAsync(string username, string password);
    public Task<UserEntity?> FindByEmailAsync(string email);
    public Task<bool> ExistsByUsernameAsync(string username);
    public Task<bool> ExistsByEmailAsync(string email);
    public Task<UserProfileEntity?> GetProfileAsync(string username);
    public Task UpdateProfileAsync(string username, UserProfileEntity profileEntity);
    public Task DeleteByUsernameAsync(string username);
}
