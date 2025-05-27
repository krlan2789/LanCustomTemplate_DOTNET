using CustomTemplate_CA_API.Application.SessionLogDomain.Commands;
using CustomTemplate_CA_API.Application.SessionLogDomain.Dtos;
using CustomTemplate_CA_API.Application.SessionLogDomain.Interfaces;
using CustomTemplate_CA_API.Application.SessionLogDomain.Mapping;
using CustomTemplate_CA_API.Application.SessionLogDomain.Queries;
using CustomTemplate_CA_API.Application.UserDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Mapping;
using CustomTemplate_CA_API.Core.Entities;

namespace CustomTemplate_CA_API.Application.SessionLogDomain.Services
{
    public class SessionLogService(ILogger<SessionLogService> logger, ISessionLogRepository sessionLogRepository, IUserRepository userRepository) : ISessionLogService
    {
        private readonly ILogger<SessionLogService> _logger = logger;
        private readonly ISessionLogRepository _sessionLogRepository = sessionLogRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task CreateSessionLog(CreateSessionLogCommand command)
        {
            _logger.LogInformation("Creating session log for user: {Username}", command.Username);
            UserEntity? currentUser = await _userRepository.FindByUsernameAsync(command.Username);
            if (currentUser == null)
            {
                _logger.LogWarning("User with username {Username} does not exist", command.Username);
                return;
            }
            await _sessionLogRepository.AddAsync(command.ToEntity(currentUser));
        }

        public async Task<IEnumerable<SessionLogDto>?> GetSessionLogs(SessionLogsByUsernameQuery query)
        {
            try
            {
                _logger.LogInformation("Retrieving session logs for user: {Username}", query.Username);
                var sessionLogs = await _sessionLogRepository.FindManyByUsernameAsync(query.Username);
                return sessionLogs?.Select(log => log.ToDto());
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving session logs for user: {Username}", query.Username);
                return null;
            }
        }
    }
}
