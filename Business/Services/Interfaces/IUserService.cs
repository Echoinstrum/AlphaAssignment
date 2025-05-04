using Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces;

public interface IUserService
{
    Task<bool> RegisterAsync(RegisterUserDto dto);
    Task<bool> LoginAsync(LoginUserDto dto);

    string? GetCurrentUserId();
}
