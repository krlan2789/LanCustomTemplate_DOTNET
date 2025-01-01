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
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly CustomTemplateDatabaseContext dbContext;
        private readonly TokenService _tokenService;

        public AuthController(ILogger<AuthController> logger, TokenService tokenService, CustomTemplateDatabaseContext context)
        {
            _logger = logger;
            dbContext = context;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = nameof(PostLogin))]
        public async Task<IResult> PostLogin(LoginUserDto dto)
        {
            try
            {
                User? currentUser = await dbContext.Users.Where(user => user.Username == dto.Username).FirstOrDefaultAsync();
                if (currentUser != null && currentUser.VerifyPassword(dto.Password))
                {
                    var token = _tokenService.GenerateToken(currentUser.Username, TimeSpan.FromDays(30));
                    Response.Headers.Append("Authorization", $"Bearer {token}");
                    return Results.Ok(new ResponseData<ResponseUserDto>("Login Successfully", token));
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

        [AllowAnonymous]
        [HttpPost("register", Name = nameof(PostRegister))]
        public async Task<IResult> PostRegister([FromBody] RegisterUserDto userDto)
        {
            try
            {
                if (userDto.Password != userDto.PasswordVerify) return Results.BadRequest(new ResponseData<object>("Password not match"));
                if (await dbContext.Users.Where(user => user.Email == userDto.Email).AnyAsync()) return Results.Conflict();
                dbContext.Users.Add(userDto.ToEntity());
                await dbContext.SaveChangesAsync();
                User? currentUser = await dbContext.Users.Where(user => user.Email == userDto.Email).FirstAsync();
                var result = await dbContext.SaveChangesAsync();
                var token = _tokenService.GenerateToken(currentUser.Username, TimeSpan.FromDays(30));
                Response.Headers.Append("Authorization", $"Bearer {token}");
                return currentUser is null && result > 0 ? Results.NotFound() : Results.Ok(
                    new ResponseData<ResponseUserDto>("Registration Successfully", token)
                );
            }
            catch (Exception e)
            {
                return Results.BadRequest(new ResponseData<object>(e.Message));
            }
        }
    }
}
