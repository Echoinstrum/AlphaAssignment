using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UserEntity : IdentityUser
{
    public string FullName { get; set; } = null!;
}
