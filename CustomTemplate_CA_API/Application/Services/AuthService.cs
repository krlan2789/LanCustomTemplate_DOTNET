using CustomTemplate_CA_API.Application.Dtos;
using CustomTemplate_CA_API.Application.Extensions;
using CustomTemplate_CA_API.Application.Interfaces.Repositories;
using CustomTemplate_CA_API.Application.Interfaces.Services;

namespace CustomTemplate_CA_API.Application.Services
{
    public class AuthService(IUserRepository userRepository) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDto?> Login(LoginUserDto dto)
        {
            var currentUser = await _userRepository.FindSecureAsync(dto.Username, dto.Password);
            return currentUser?.ToDto();
        }

        public async Task<UserDto?> Register(RegisterUserDto dto)
        {
            if (await _userRepository.ExistsByEmailAsync(dto.Email)) return null;
            var results = await _userRepository.AddAsync(dto.ToEntity());
            if (!string.IsNullOrEmpty(results)) return null;
            var currentUser = await _userRepository.FindByEmailAsync(dto.Email);
            return currentUser?.ToDto();
        }
    }
}
