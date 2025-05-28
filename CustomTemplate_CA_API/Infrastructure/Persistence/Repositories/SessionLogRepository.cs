using CustomTemplate_CA_API.Application.SessionLogDomain.Interfaces;
using CustomTemplate_CA_API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_API.Infrastructure.Persistence.Repositories
{
    public class SessionLogRepository(AppDatabaseContext dbContext) : BaseRepository(dbContext), ISessionLogRepository
    {
        public async Task<IEnumerable<SessionLogEntity>?> FindManyByUsernameAsync(string username)
        {
            return await _dbContext.UserSessionLogs
                .Where(e => e.User != null && e.User.Username == username)
                .ToListAsync();
        }
    }
}
