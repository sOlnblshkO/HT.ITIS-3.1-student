using System.Security.Claims;
using Dotnet.Homeworks.Domain.Repositories;
using Dotnet.Homeworks.Features.RequestTypes;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;

namespace Dotnet.Homeworks.Features.UserManagement.Common;

public class AdminPermissionChecker : IPermissionChecker<IAdminRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly HttpContext _httpContext;

    public AdminPermissionChecker(
        IHttpContextAccessor httpContextAccessor
    )
    {
        _httpContext = httpContextAccessor.HttpContext;
    }
    
    public async Task<PermissionResult> Validate(IAdminRequest request)
    {
        var claimRole = _httpContext.User.Claims
            .FirstOrDefault(claim=>claim.Type == ClaimTypes.Role)?.Value;

        return claimRole != "Admin" ? new PermissionResult(false, "Access denied") : new PermissionResult(true);
    }
}