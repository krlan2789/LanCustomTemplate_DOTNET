using CustomTemplate_CA_API.Application.Extensions;
using CustomTemplate_CA_API.Application.Interfaces.Repositories;
using CustomTemplate_CA_API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_API.Infrastructure.Persistence.Repositories
{
    public class UserRepository(IDbContextFactory<AppDatabaseContext> dbContextFactory) : IUserRepository
    {
        private readonly IDbContextFactory<AppDatabaseContext> _dbContextFactory = dbContextFactory;

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
                .Where(u => u.Username == username && u.VerifyPassword(password))
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

        public async Task<string?> AddAsync(UserEntity entity)
        {
            if (await ExistsByEmailAsync(entity.Email)) return "Email already registered";
            using var dbContext = _dbContextFactory.CreateDbContext();
            dbContext.Users.Add(entity);
            await dbContext.SaveChangesAsync();
            return null;
        }

        public async Task<string?> DeleteByUsernameAsync(string username)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var user = await dbContext.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return "User not found";
            dbContext.Users.Remove(user);
            dbContext.UserProfiles.Remove(user.Profile!);
            await dbContext.SaveChangesAsync();
            return null;
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

        public async Task<string?> UpdateProfileAsync(string username, UserProfileEntity entity)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var user = await dbContext.Users
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
            if (user == null) return "User not found";
            user.Profile = entity;
            dbContext.Users.Update(user);
            dbContext.UserProfiles.Update(entity);
            await dbContext.SaveChangesAsync();
            return null;
        }
    }
}
