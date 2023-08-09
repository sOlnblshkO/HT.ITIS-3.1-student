using System.Security.Claims;
using Dotnet.Homeworks.Domain.Repositories;
using Dotnet.Homeworks.Features.RequestTypes;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;

namespace Dotnet.Homeworks.Features.Users.Common;

public class ClientPermissionChecker : IPermissionChecker<IClientRequest>
    {
        private readonly IUserRepository _userRepository;
        private readonly HttpContext _httpContext;

    public ClientPermissionChecker(
        IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository
        )
    {
        _userRepository = userRepository;
        _httpContext = httpContextAccessor.HttpContext;
    }
    
    public async Task<PermissionResult> Validate(IClientRequest request)
    {
        var claimGuid = _httpContext.User.Claims
            .FirstOrDefault(claim=>claim.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid id = new Guid();
        if (!Guid.TryParse(claimGuid, out id)) 
            return new PermissionResult(false, "No id provided");

        var user = await _userRepository.GetUserByGuid(id);
        
        if (user.Id != request.Guid)
            return new PermissionResult(false, "Access denied");
            
        return new PermissionResult(true, null);
    }
}