using Business.Dtos;
using Business.Services.Interfaces;
using Data.Entities;
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

    public async Task<string?> LoginAsync(LoginUserDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            return user?.Id;
        }
        return null;
    }

    public string? GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}


