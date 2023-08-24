using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.MainProject.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

[ApiController]
public class UserManagementController : ControllerBase
{
    [HttpPost("user")]
    public Task<IActionResult> CreateUser(RegisterUserDto userDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("profile/{guid}")]
    public Task<IActionResult> GetProfile(Guid guid, CancellationToken cancellationToken) 
    {
        throw new NotImplementedException();
    }

    [HttpGet("users")]
    public Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("profile/{guid:guid}")]
    public Task<IActionResult> DeleteProfile(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPut("profile")]
    public Task<IActionResult> UpdateProfile(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("user/{guid:guid}")]
    public Task<IActionResult> DeleteUser(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}