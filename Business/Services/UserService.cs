using Business.Dtos;
using Business.Services.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> RegisterAsync(RegisterUserDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
        {
            return false;
        }

        var user = new UserEntity
        {
            FullName = dto.FullName,
            UserName = dto.Email,
            Email = dto.Email,
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        return result.Succeeded;
    }
    //Got help from ChatGPT4o here,  Since i didn't know how i should get access to showing the logged in users "Fullname" in the view.
    //Instead of using the the SignInManager.PasswordSignInAsync( that signs in using the default flaims), this build a custom ClaimsIdentity
    //manually and uses HttpContext.SignInAsync. And trough that, getting more control over what claims that are included, and in my case, the FullName which i wanted to show in the settings-gear.
    public async Task<bool> LoginAsync(LoginUserDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if(user == null)
        {
            return false;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if(!result.Succeeded)
        {
            return false;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("FullName", user.FullName ?? user.Email)
        };

        var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
        var principal = new ClaimsPrincipal(identity);
        await _httpContextAccessor.HttpContext!.SignInAsync(IdentityConstants.ApplicationScheme, principal);
        return true;
    }

    public string? GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}


