using CustomTemplate_CA_API.Application.UserDomain.Interfaces;
using CustomTemplate_CA_API.Core.Entities;
using CustomTemplate_CA_API.Core.Helper;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_API.Infrastructure.Persistence.Repositories;

public class UserRepository(IDbContextFactory<AppDatabaseContext> dbContextFactory)
    : BaseRepository<UserEntity>(dbContextFactory), IUserRepository
{
    public async Task<UserEntity?> FindByUsernameAsync(string username)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        return await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Username == username)
            .Select(u => u)
            .FirstOrDefaultAsync();
    }

    public async Task<UserEntity?> FindSecureAsync(string username, string password)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        return await dbContext.Users
            .Where(u => u.Username == username && password.VerifyHashed(u.PasswordHash))
            .Select(u => u)
            .FirstOrDefaultAsync();
    }

    public async Task<UserEntity?> FindByEmailAsync(string email)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        return await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Email == email)
            .Select(u => u)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        return await dbContext
            .Users.Where(u => u.Username == username)
            .AnyAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        return await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Email == email)
            .AnyAsync();
    }

    public Task<UserProfileEntity?> GetProfileAsync(string username)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        return dbContext.UserProfiles
            .AsNoTracking()
            .Include(u => u.User)
            .Where(u => u.User!.Username == username)
            .Select(u => u)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateProfileAsync(string username, UserProfileEntity profileEntity)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var user = await dbContext.Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync()
            ?? throw new InvalidOperationException("User not found");
        user.Profile = profileEntity;
        dbContext.Users.Update(user);
        dbContext.UserProfiles.Update(profileEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteByUsernameAsync(string username)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var user = await dbContext.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.Username == username)
            ?? throw new InvalidOperationException("User not found");
        dbContext.Users.Remove(user);
        dbContext.UserProfiles.Remove(user.Profile!);
        await dbContext.SaveChangesAsync();
    }

    public async override Task AddAsync(UserEntity entity)
    {
        if (await ExistsByEmailAsync(entity.Email)) throw new InvalidOperationException("Email already registered");
        if (await ExistsByUsernameAsync(entity.Username)) throw new InvalidOperationException("Username already registered");
        using var dbContext = _dbContextFactory.CreateDbContext();
        dbContext.Users.Add(entity);
        await dbContext.SaveChangesAsync();
    }
}
