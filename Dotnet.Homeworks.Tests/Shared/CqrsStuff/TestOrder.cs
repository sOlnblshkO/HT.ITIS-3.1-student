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
    public static async Task<Result<CreateOrderDto>> CreateOrderAsync(IMediator mediator, params Guid[] productsIds)
    {
        var createOrder = new CreateOrderCommand(productsIds);
        return await mediator.Send(createOrder);
    }

    public static async Task<Result<GetOrderDto>> GetOrderAsync(IMediator mediator, Guid orderId)
    {
        var getOrder = new GetOrderQuery(orderId);
        return await mediator.Send(getOrder);
    }

    public static async Task<Result<GetOrdersDto>> GetOrdersAsync(IMediator mediator)
    {
        var getOrders = new GetOrdersQuery();
        return await mediator.Send(getOrders);
    }

    public static async Task<Result> DeleteOrderAsync(IMediator mediator, Guid orderId)
    {
        var deleteOrder = new DeleteOrderByGuidCommand(orderId);
        return await mediator.Send(deleteOrder);
    }

    public static async Task<Result> UpdateOrderAsync(IMediator mediator, Guid orderId, IEnumerable<Guid> productsIds)
    {
        var updateOrder = new UpdateOrderCommand(orderId, productsIds);
        return await mediator.Send(updateOrder);
    }
}