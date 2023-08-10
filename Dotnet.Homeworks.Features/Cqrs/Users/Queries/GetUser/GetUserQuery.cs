using Dotnet.Homeworks.Features.RequestTypes;

namespace Dotnet.Homeworks.Features.Cqrs.Users.Queries.GetUser;

public class GetUserQuery : IClientRequest // , ???
{
    public Guid Guid { get; init; }

    public GetUserQuery(Guid guid)
    {
        Guid = guid;
    }
};