namespace Dotnet.Homeworks.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(IEnumerable<Guid> ProductsIds); // TODO: implement interface