using System.Collections.Concurrent;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Tests.Shared.CqrsStuff;

public class OrderRepositoryMock : IOrderRepository
{
    private readonly ConcurrentDictionary<Guid, Order> _orders;

    public OrderRepositoryMock()
    {
        _orders = new ConcurrentDictionary<Guid, Order>();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersFromUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_orders.Values.Where(o => o.OrdererId == userId));
    }

    public async Task<Order?> GetOrderByGuidAsync(Guid orderGuid, CancellationToken cancellationToken)
    {
        if (_orders.TryGetValue(orderGuid, out var order))
            return await Task.FromResult(order);

        return null;
    }

    public async Task DeleteOrderByGuidAsync(Guid orderGuid, CancellationToken cancellationToken)
    {
        _orders.Remove(orderGuid, out _);
        await Task.CompletedTask;
    }

    public async Task UpdateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        _orders[order.Id] = order;
        await Task.CompletedTask;
    }

    public async Task<Guid> InsertOrderAsync(Order order, CancellationToken cancellationToken)
    {
        _orders.AddOrUpdate(order.Id, order, (_, _) => order);
        return await Task.FromResult(order.Id);
    }
}
