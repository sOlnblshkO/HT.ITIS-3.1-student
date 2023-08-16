namespace Dotnet.Homeworks.Features.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(Guid OrderId, IEnumerable<Guid> ProductsIds); // TODO: implement interface