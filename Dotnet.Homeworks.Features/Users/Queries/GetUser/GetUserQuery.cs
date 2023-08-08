using Dotnet.Homeworks.Features.RequestTypes;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Features.Users.Queries.GetUser;

public class GetUserQuery: IQuery<GetUserDto>, IClientRequest
{
    public GetUserQuery(Guid guid)
    {
        Guid = guid;
    }
    public Guid Guid { get; init; }
    
    public Result<GetUserDto> Result { get; init; }
};