using CustomTemplate.API.Data;
using CustomTemplate.API.Dtos;
using CustomTemplate.API.Entities;
using CustomTemplate.API.Mapping;
using CustomTemplate.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly LanDatabaseContext dbContext;
        private readonly JwtTokenService _tokenService;

        public UserController(ILogger<UserController> logger, JwtTokenService tokenService, LanDatabaseContext context)
        {
            _logger = logger;
            dbContext = context;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("profile")]
        [EndpointSummary("Get User Profile (Authorized)")]
        [EndpointDescription("Get authorized user profile.")]
        [ProducesResponseType<ResponseData<ResponseUserDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status401Unauthorized)]
        public async Task<IResult> GetProfileSelf()
        {
            try
            {
                var username = _tokenService.GetUsername(HttpContext);
                User? currentUser = await dbContext.Users.Where(user => user.Username == username).FirstAsync();
                if (currentUser != null)
                {
                    return Results.Ok(new ResponseData<ResponseUserDto>("Success", currentUser.ToResponseDto()));
                }
                else
                {
                    return Results.Unauthorized();
                }
            }
            catch (Exception e)
            {
                return Results.BadRequest(new ResponseData<object>(e.Message));
            }
        }

        [HttpGet("profile/{Username}")]
        [EndpointSummary("Get User Profile")]
        [EndpointDescription("Get user profile.")]
        [ProducesResponseType<ResponseData<ResponseUserDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseError<object>>(StatusCodes.Status404NotFound)]
        public async Task<IResult> GetProfile(string Username)
        {
            try
            {
                User? currentUser = await dbContext.Users.Where(user => user.Username == Username).FirstOrDefaultAsync();
                if (currentUser != null)
                {
                    return Results.Ok(new ResponseData<ResponseUserDto>("Success", currentUser.ToResponseDto()));
                }
                else
                {
                    return Results.NotFound(new ResponseData<object>("User not found"));
                }
            }
            catch (Exception e)
            {
                return Results.BadRequest(new ResponseData<object>(e.Message));
            }
        }
    }
}
