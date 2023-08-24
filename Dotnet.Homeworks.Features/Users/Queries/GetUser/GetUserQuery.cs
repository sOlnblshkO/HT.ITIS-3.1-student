using Dotnet.Homeworks.Infrastructure.Validation.RequestTypes;

namespace Dotnet.Homeworks.Features.Users.Queries.GetUser;

public class GetUserQuery : IClientRequest //TODO: Inherit certain interface 
{
    public Guid Guid { get; init; }

    public GetUserQuery(Guid guid)
    {
        Guid = guid;
    }
};