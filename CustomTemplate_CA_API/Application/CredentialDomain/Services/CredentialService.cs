using CustomTemplate_CA_API.Application.CredentialDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Commands;
using CustomTemplate_CA_API.Application.UserDomain.Dtos;
using CustomTemplate_CA_API.Application.UserDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Mapping;

namespace CustomTemplate_CA_API.Application.CredentialDomain.Services;

public class CredentialService(ILogger<ICredentialService> logger, IUserRepository userRepository) : ICredentialService
{
    private readonly ILogger<ICredentialService> _logger = logger;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDto?> Login(LoginUserCommand dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                _logger.LogWarning("Username or password cannot be empty");
                return null;
            }
            var currentUser = await _userRepository.FindSecureAsync(dto.Username, dto.Password);
            return currentUser?.ToDto();
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating login credentials for {Username}", dto.Username);
            return null;
        }
    }

    public async Task<UserDto?> Register(RegisterUserCommand dto)
    {
        try
        {
            if (await _userRepository.ExistsByEmailAsync(dto.Email))
            {
                _logger.LogWarning("User with email {Email} already exists", dto.Email);
                return null;
            }
            await _userRepository.AddAsync(dto.ToEntity());
            var currentUser = await _userRepository.FindByEmailAsync(dto.Email);
            return currentUser?.ToDto();
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering user with email {Email}", dto.Email);
            return null;
        }
    }
}
