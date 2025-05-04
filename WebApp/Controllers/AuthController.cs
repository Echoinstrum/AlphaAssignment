using Business.Dtos;
using Business.Services.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<UserEntity> _signInManager;

        public AuthController(IUserService userService, SignInManager<UserEntity> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var success = await _userService.LoginAsync(new LoginUserDto { Email = email, Password = password });

            if (success)
                return RedirectToAction("Index", "Projects");

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

        //Got help from ChatGPT4o with the Logout function. Wasn't quite sure how to end a session correctly. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
