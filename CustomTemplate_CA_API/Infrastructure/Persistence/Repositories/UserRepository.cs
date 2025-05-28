using CustomTemplate_CA_API.Application.UserDomain.Interfaces;
using CustomTemplate_CA_API.Core.Entities;
using CustomTemplate_CA_API.Core.Helper;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_API.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDatabaseContext dbContext)
    : BaseRepository(dbContext), IUserRepository
{
    public async Task<UserEntity?> FindByUsernameAsync(string username)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Username == username)
            .Select(u => u)
            .FirstOrDefaultAsync();
    }

    public async Task<UserEntity?> FindSecureAsync(string username, string password)
    {
        return await _dbContext.Users
            .Where(u => u.Username == username && password.VerifyHashed(u.PasswordHash))
            .Select(u => u)
            .FirstOrDefaultAsync();
    }

    public async Task<UserEntity?> FindByEmailAsync(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Email == email)
            .Select(u => u)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _dbContext
            .Users.Where(u => u.Username == username)
            .AnyAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Email == email)
            .AnyAsync();
    }

    public Task<UserProfileEntity?> GetProfileAsync(string username)
    {
        return _dbContext.UserProfiles
            .AsNoTracking()
            .Include(u => u.User)
            .Where(u => u.User!.Username == username)
            .Select(u => u)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateProfileAsync(string username, UserProfileEntity profileEntity)
    {
        var user = await _dbContext.Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync()
            ?? throw new InvalidOperationException("User not found");
        user.Profile = profileEntity;
        _dbContext.Users.Update(user);
        _dbContext.UserProfiles.Update(profileEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteByUsernameAsync(string username)
    {
        var user = await _dbContext.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.Username == username)
            ?? throw new InvalidOperationException("User not found");
        _dbContext.Users.Remove(user);
        _dbContext.UserProfiles.Remove(user.Profile!);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(UserEntity entity)
    {
        if (await ExistsByEmailAsync(entity.Email)) throw new InvalidOperationException("Email already registered");
        if (await ExistsByUsernameAsync(entity.Username)) throw new InvalidOperationException("Username already registered");
        _dbContext.Users.Add(entity);
        await _dbContext.SaveChangesAsync();
    }
}
