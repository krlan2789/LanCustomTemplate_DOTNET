using CustomTemplate_CA_API.Application.UserDomain.Commands;
using CustomTemplate_CA_API.Application.UserDomain.Dtos;

namespace CustomTemplate_CA_API.Application.CredentialDomain.Interfaces
{
    public interface ICredentialService
    {
        public Task<UserDto?> Login(LoginUserCommand dto);
        public Task<UserDto?> Register(RegisterUserCommand dto);
    }
}
