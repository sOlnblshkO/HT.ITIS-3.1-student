namespace Dotnet.Homeworks.Features.Orders.Queries.GetOrder;

public class GetOrderQuery // TODO: implement interface
{
    public GetOrderQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}