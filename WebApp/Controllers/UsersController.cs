using Business.Dtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterAsync(dto);
            if (result)
            {
                return Ok("User was succesfully registered");
            }
            return BadRequest("User registration failed, no user registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var userId = await _userService.LoginAsync(dto);
            if(userId != null)
            {
                return Ok($"User with userId: {userId}, succesfully logged in");
            }
            return Unauthorized("Email or Password was invalid");
        }
    }
}
