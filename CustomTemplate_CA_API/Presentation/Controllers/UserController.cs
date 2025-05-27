using CustomTemplate_CA_API.Application.Common.Dtos;
using CustomTemplate_CA_API.Application.CredentialDomain.Interfaces;
using CustomTemplate_CA_API.Application.UserDomain.Commands;
using CustomTemplate_CA_API.Application.UserDomain.Dtos;
using CustomTemplate_CA_API.Application.UserDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomTemplate_CA_API.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ILogger<UserController> logger, ITokenService tokenService, IUserService userService) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IUserService _userService = userService;

        [Authorize]
        [HttpGet("profile")]
        [EndpointSummary("Get User Profile (Authorized)")]
        [EndpointDescription("Get authorized user profile.")]
        [ProducesResponseType<ResponseData<UserProfileDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status401Unauthorized)]
        public async Task<IResult> GetProfileSelf()
        {
            try
            {
                var username = "" + _tokenService.GetUsername(HttpContext);
                var currentUser = await _userService.GetProfile(new(username));
                if (currentUser != null)
                {
                    return Results.Ok(new ResponseData<UserProfileDto>("Success", currentUser));
                }
                else
                {
                    _logger.LogError("You are not authorized");
                    return Results.Unauthorized();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during getting profile");
                return Results.BadRequest(new ResponseData<object>(e.Message));
            }
        }

        [HttpGet("profile/{Username}")]
        [EndpointSummary("Get User Profile")]
        [EndpointDescription("Get user profile.")]
        [ProducesResponseType<ResponseData<UserProfileDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status404NotFound)]
        public async Task<IResult> GetProfile(string Username)
        {
            try
            {
                var currentUser = await _userService.GetProfile(new(Username));
                if (currentUser != null)
                {
                    return Results.Ok(new ResponseData<UserProfileDto>("Success", currentUser));
                }
                else
                {
                    _logger.LogError("User not found");
                    return Results.NotFound(new ResponseData<object>("User not found"));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during getting profile");
                return Results.BadRequest(new ResponseData<object>(e.Message));
            }
        }

        [Authorize]
        [HttpPut("profile")]
        [EndpointSummary("Update User Profile (Authorized)")]
        [EndpointDescription("Update authorized user profile.")]
        [ProducesResponseType<ResponseData<UserProfileDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status401Unauthorized)]
        public async Task<IResult> UpdateProfile([FromBody] UpdateUserProfileCommand cmd)
        {
            try
            {
                var username = "" + _tokenService.GetUsername(HttpContext);
                var currentUser = await _userService.UpdateProfile(cmd);
                if (currentUser != null)
                {
                    return Results.Ok(new ResponseData<UserProfileDto>("Success", currentUser));
                } else
                {
                    _logger.LogError("You are not authorized");
                    return Results.Unauthorized();
                }
            } catch (Exception e)
            {
                _logger.LogError(e, "Error during updating profile");
                return Results.BadRequest(new ResponseData<object>(e.Message));
            }
        }
    }
}
