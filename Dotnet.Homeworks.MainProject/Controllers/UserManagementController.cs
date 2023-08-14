using System.Security.Claims;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.MainProject.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

public class UserManagementController : ControllerBase
{
    [HttpPost("user")]
    public async Task<IActionResult> CreateUser(RegisterUserDto userDto)
    {
        throw new NotImplementedException();
    }

    [HttpGet("profile/{guid}")]
    public async Task<IActionResult> GetProfile(Guid guid) 
    {
        throw new NotImplementedException();
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("profile/{guid:guid}")]
    public async Task<IActionResult> DeleteProfile(Guid guid)
    {
        throw new NotImplementedException();
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(User user)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("user/{guid:guid}")]
    public async Task<IActionResult> DeleteUser(Guid guid)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(Guid guid)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, guid.ToString()),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimPrincipal = new ClaimsPrincipal(claimIdentity);

        await HttpContext.SignInAsync(claimPrincipal);

        return Ok();
    }
}