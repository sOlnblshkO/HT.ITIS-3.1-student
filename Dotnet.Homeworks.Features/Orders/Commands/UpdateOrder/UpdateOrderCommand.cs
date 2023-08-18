namespace Dotnet.Homeworks.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand // TODO: implement interface
{
    public UpdateOrderCommand(Guid orderId, IEnumerable<Guid> productsIds)
    {
        OrderId = orderId;
        ProductsIds = productsIds;
    }

    public Guid OrderId { get; init; }
    public IEnumerable<Guid> ProductsIds { get; init; }
}