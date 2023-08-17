using Dotnet.Homeworks.Features.Orders.Commands.CreateOrder;
using Dotnet.Homeworks.Features.Orders.Commands.DeleteOrder;
using Dotnet.Homeworks.Features.Orders.Commands.UpdateOrder;
using Dotnet.Homeworks.Features.Orders.Queries.GetOrder;
using Dotnet.Homeworks.Features.Orders.Queries.GetOrders;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Tests.Shared.CqrsStuff;

public static class TestOrder
{
    private static CreateOrderCommand GetCreateCommand(IEnumerable<Guid> productIds) => new(productIds);

    private static UpdateOrderCommand GetUpdateCommand(Guid orderId, IEnumerable<Guid> productsIds) =>
        new(orderId, productsIds);

    private static DeleteOrderByGuidCommand GetDeleteCommand(Guid id) => new(id);

    private static GetOrdersQuery GetGetAllQuery() => new();

    private static GetOrderQuery GetGetOneQuery(Guid id) => new(id);
    
    public static async Task<Result<CreateOrderDto>> CreateOrderAsync(IMediator mediator, params Guid[] productsIds)
    {
        var createOrder = GetCreateCommand(productsIds);
        return await mediator.Send(createOrder);
    }

    public static async Task<Result<GetOrderDto>> GetOrderAsync(IMediator mediator, Guid orderId)
    {
        var getOrder = GetGetOneQuery(orderId);
        return await mediator.Send(getOrder);
    }

    public static async Task<Result<GetOrdersDto>> GetOrdersAsync(IMediator mediator)
    {
        var getOrders = GetGetAllQuery();
        return await mediator.Send(getOrders);
    }

    public static async Task<Result> DeleteOrderAsync(IMediator mediator, Guid orderId)
    {
        var deleteOrder = GetDeleteCommand(orderId);
        return await mediator.Send(deleteOrder);
    }

    public static async Task<Result> UpdateOrderAsync(IMediator mediator, Guid orderId, IEnumerable<Guid> productsIds)
    {
        var updateOrder = GetUpdateCommand(orderId, productsIds);
        return await mediator.Send(updateOrder);
    }
}