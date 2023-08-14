namespace Dotnet.Homeworks.Features.Orders.Queries.GetOrder;

public record GetOrderDto(Guid Id, IEnumerable<Guid> ProductsIds);