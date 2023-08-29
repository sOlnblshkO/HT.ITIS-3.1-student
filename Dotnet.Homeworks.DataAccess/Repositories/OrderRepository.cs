using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllOrdersFromUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetOrderByGuidAsync(Guid orderId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOrderByGuidAsync(Guid orderId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> InsertOrderAsync(Order order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}