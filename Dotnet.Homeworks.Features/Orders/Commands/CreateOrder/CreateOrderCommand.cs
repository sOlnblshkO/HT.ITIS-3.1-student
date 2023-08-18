namespace Dotnet.Homeworks.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand // TODO: implement interface
{
    public CreateOrderCommand(IEnumerable<Guid> productsIds)
    {
        ProductsIds = productsIds;
    }

    public IEnumerable<Guid> ProductsIds { get; init; }
}