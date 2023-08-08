using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;

namespace Dotnet.Homeworks.Features.Users.Commands.CreateUser;

public class CreateUserPermissionChecker :  IPermissionChecker<CreateUserCommand>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CreateUserPermissionChecker(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<PermissionResult> Validate(CreateUserCommand request)
    {
        var h = _httpContextAccessor.HttpContext;
        return new PermissionResult(true, null);
    }
}