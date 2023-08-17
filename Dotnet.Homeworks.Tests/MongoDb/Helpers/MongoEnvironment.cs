using System.Security.Claims;
using Dotnet.Homeworks.Mediator;
using Dotnet.Homeworks.Tests.Shared.CqrsStuff;
using Microsoft.AspNetCore.Http;

namespace Dotnet.Homeworks.Tests.MongoDb.Helpers;

public class MongoEnvironment
{
    public MongoEnvironment(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        Mediator = mediator;
        HttpContextAccessor = httpContextAccessor;
    }

    public IMediator Mediator { get; }

    private IHttpContextAccessor HttpContextAccessor { get; }

    public void LogOutCurrentUser() => HttpContextAccessor.HttpContext!.User = new ClaimsPrincipal();

    /// <summary>
    ///     Creates a user and authorizes under its Guid in the current Context
    /// </summary>
    public async Task LogInNewUserAsync()
    {
        var userRes = await TestUser.CreateUserAsync(Mediator);
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, userRes.Value!.Guid.ToString()) };
        var claimsIdentity = new ClaimsIdentity(claims);
        HttpContextAccessor.HttpContext!.User = new ClaimsPrincipal(claimsIdentity);
    }
}