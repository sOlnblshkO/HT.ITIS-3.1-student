using System.Security.Claims;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

public class UserManagementController : ControllerBase
{
    [HttpPost("user")]
    public async Task<IActionResult> CreateUser(string name, string email)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("user")]
    public async Task RegisterUser(RegisterUserDto userDto, IRegistrationService registrationService)
    {
        await registrationService.RegisterAsync(userDto);
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

    [HttpGet("login")]
    public async Task<IActionResult> Login()
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, "Email@example.ru"),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimPrincipal = new ClaimsPrincipal(claimIdentity);

        await HttpContext.SignInAsync(claimPrincipal);
        
        return Ok();
    }
}