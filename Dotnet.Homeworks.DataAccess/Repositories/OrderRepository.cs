using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetAllOrdersFromUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Order?> GetOrderByGuidAsync(Guid orderGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteOrderByGuidAsync(Guid orderGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> InsertOrderAsync(Order order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}