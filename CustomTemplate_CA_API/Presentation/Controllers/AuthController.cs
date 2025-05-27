using CustomTemplate_CA_API.Application.Common.Dtos;
using CustomTemplate_CA_API.Application.CredentialDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Commands;
using CustomTemplate_CA_API.Application.UserDomain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomTemplate_CA_API.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ILogger<AuthController> logger, ITokenService tokenService, ICredentialService authService) : ControllerBase
    {
        private readonly ILogger<AuthController> _logger = logger;
        private readonly ITokenService _tokenService = tokenService;
        private readonly ICredentialService _authService = authService;

        [AllowAnonymous]
        [HttpPost("login")]
        [EndpointSummary("Login")]
        [EndpointDescription("Login to get token for credential.")]
        [ProducesResponseType<ResponseData<UserDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status404NotFound)]
        public async Task<IResult> PostLogin(LoginUserCommand dto)
        {
            try
            {
                var currentUser = await _authService.Login(dto);
                if (currentUser != null && currentUser?.Username?.Length > 0)
                {
                    var token = _tokenService.GenerateToken(currentUser.Username, TimeSpan.FromDays(30));
                    Response.Headers.Append("Authorization", $"Bearer {token}");
                    return Results.Ok(new ResponseData<UserDto>("Login Successfully", token));
                }
                else
                {
                    _logger.LogError("User not found");
                    return Results.NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during login");
                return Results.BadRequest(new ResponseData<object>(e.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [EndpointSummary("Register")]
        [EndpointDescription("Create new account to use authorized endpoints.")]
        [ProducesResponseType<ResponseData<UserDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status400BadRequest)]
        public async Task<IResult> PostRegister([FromBody] RegisterUserCommand dto)
        {
            try
            {
                if (dto.Password == dto.PasswordVerify)
                {
                    var currentUser = await _authService.Register(dto);
                    if (currentUser is not null && !string.IsNullOrEmpty(currentUser.Username))
                    {
                        var token = _tokenService.GenerateToken(currentUser.Username, TimeSpan.FromDays(30));
                        Response.Headers.Append("Authorization", $"Bearer {token}");
                        return Results.Ok(new ResponseData<UserDto>("Registration Successfully", token));
                    }
                    else
                    {
                        _logger.LogError("Email already registered");
                        return Results.Conflict(new ResponseData<object>("Email already registered"));
                    }
                }
                else
                {
                    _logger.LogError("Password not match");
                    return Results.BadRequest(new ResponseData<object>("Password not match"));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during registration");
                return Results.BadRequest(new ResponseData<object>(e.Message));
            }
        }
    }
}
