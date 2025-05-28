using CustomTemplate_CA_API.Core.Entities;
using CustomTemplate_CA_API.Core.Repositories;

namespace CustomTemplate_CA_API.Application.SessionLogDomain.Interfaces;

public interface ISessionLogRepository : IBaseRepository
{
    public Task<IEnumerable<SessionLogEntity>?> FindManyByUsernameAsync(string username);
}
