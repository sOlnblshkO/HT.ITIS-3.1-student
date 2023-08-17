using System.Security.Claims;
using Dotnet.Homeworks.Mediator;
using Dotnet.Homeworks.Tests.Shared.CqrsStuff;
using Microsoft.AspNetCore.Http;

namespace Dotnet.Homeworks.Tests.MongoDb.Helpers;

public class MongoEnvironment
{
    public MongoEnvironment(IMediator mediator,
        IHttpContextAccessor httpContextAccessor, Guid? contextUserId)
    {
        Mediator = mediator;
        HttpContextAccessor = httpContextAccessor;
        ContextUserId = contextUserId;
    }

    public IMediator Mediator { get; }

    private IHttpContextAccessor HttpContextAccessor { get; }

    /// <summary>
    ///     Null if builder wasn't called WithFakeUserInContext. Then updates after LogInNewUserAsync call.
    /// </summary>
    public Guid? ContextUserId { get; private set; }

    public void LogOutCurrentUser()
    {
        HttpContextAccessor.HttpContext!.User = new ClaimsPrincipal();
    }

    /// <summary>
    ///     Creates a user and authorizes under its Guid in the current Context
    /// </summary>
    public async Task LogInNewUserAsync()
    {
        var userRes = await TestUser.CreateUserAsync(Mediator);
        ContextUserId = userRes.Value!.Guid;
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, ContextUserId.Value.ToString()) };
        var claimsIdentity = new ClaimsIdentity(claims);
        HttpContextAccessor.HttpContext!.User = new ClaimsPrincipal(claimsIdentity);
    }
}