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
        private readonly CustomTemplateDatabaseContext dbContext;
        private readonly TokenService _tokenService;

        public UserController(ILogger<UserController> logger, TokenService tokenService, CustomTemplateDatabaseContext context)
        {
            _logger = logger;
            dbContext = context;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("profile", Name = nameof(GetProfileSelf))]
        public async Task<IResult> GetProfileSelf()
        {
            try
            {
                string username = _tokenService.GetUserIdFromToken(HttpContext);
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

        [HttpGet("profile/{Username}", Name = nameof(GetProfile))]
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
