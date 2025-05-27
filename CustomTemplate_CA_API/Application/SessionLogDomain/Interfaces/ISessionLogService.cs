using CustomTemplate_CA_API.Application.SessionLogDomain.Commands;
using CustomTemplate_CA_API.Application.SessionLogDomain.Dtos;
using CustomTemplate_CA_API.Application.SessionLogDomain.Queries;

namespace CustomTemplate_CA_API.Application.SessionLogDomain.Interfaces;

public interface ISessionLogService
{
    public Task<IEnumerable<SessionLogDto>?> GetSessionLogs(SessionLogsByUsernameQuery query);
    public Task CreateSessionLog(CreateSessionLogCommand command);
}
