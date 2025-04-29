using Business.Dtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var loginDto = new LoginUserDto
            {
                Email = email,
                Password = password
            };

            var userId = await _userService.LoginAsync(loginDto);

            if (userId != null)
            {
                HttpContext.Session.SetString("UserId", userId);
                return RedirectToAction("Index", "Projects");
            }

            ViewData["ErrorMessage"] = "Invalid email or password";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string fullName, string email, string password, string confirmPassword)
        {
            var dto = new RegisterUserDto
            {
                FullName = fullName,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var success = await _userService.RegisterAsync(dto);

            if(!success)
            {
                return RedirectToAction("Login");
            }

            ViewData["ErrorMessage"] = "Registration failed. Please check the details";
            return View();
        }
    }
}
