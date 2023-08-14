using Dotnet.Homeworks.Features.Orders.Queries.GetOrder;

namespace Dotnet.Homeworks.Features.Orders.Queries.GetOrders;

public record GetOrdersDto(IEnumerable<GetOrderDto> Orders);