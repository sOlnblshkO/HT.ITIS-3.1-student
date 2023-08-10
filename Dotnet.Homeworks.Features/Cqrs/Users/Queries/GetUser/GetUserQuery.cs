namespace Dotnet.Homeworks.Features.Users.Queries.GetUser;

public class GetUserQuery
{
    public GetUserQuery(Guid guid)
    {
        Guid = guid;
    }
    public Guid Guid { get; init; }
};