using Dotnet.Homeworks.Features.RequestTypes;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Features.UserManagement.Queries.GetAllUsers;

public class GetAllUsersQuery : IQuery<IList<GetAllUsersDto>>, IAdminRequest
{
    public Result<IList<GetAllUsersDto>> Result { get; init; }
}