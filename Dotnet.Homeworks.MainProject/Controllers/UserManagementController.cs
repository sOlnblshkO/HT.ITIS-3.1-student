using System.Security.Claims;
using Dotnet.Homeworks.Features.UserManagement.Queries.GetAllUsers;
using Dotnet.Homeworks.Features.Users.Commands.CreateUser;
using Dotnet.Homeworks.Mediator;
using Dotnet.Homeworks.Features.Users.Queries.GetUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

[Controller]
public class UserManagementController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserManagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //TODO: в зависимости от кода ошибки отправлять соответствующий статус
    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser(string name, string email)
    {
        var result = await _mediator.Send(new CreateUserCommand(name, email));
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpGet("profile/{guid}")]
    public async Task<IActionResult> GetProfile(Guid guid) 
    {
        return Ok(await _mediator.Send(new GetUserQuery(guid)));   
    }

    [HttpPost("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }


    [HttpGet("login")]
    public async Task<IActionResult> Login()
    {
        var claims = new List<Claim>() { new Claim(ClaimTypes.Email, "Email@lol.ru") };
        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimPrincipal = new ClaimsPrincipal(claimIdentity);

        await HttpContext.SignInAsync(claimPrincipal);
        
        return Ok();
    }

    [Authorize("HasEmail")]
    [HttpGet("check")]
    public async Task<IActionResult> CheckLogin()
    {
        return Ok("Success");
    }
}