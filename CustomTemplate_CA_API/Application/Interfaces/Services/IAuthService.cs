using CustomTemplate_CA_API.Application.Dtos;

namespace CustomTemplate_CA_API.Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<UserDto?> Login(LoginUserDto dto);
        public Task<UserDto?> Register(RegisterUserDto dto);
    }
}
