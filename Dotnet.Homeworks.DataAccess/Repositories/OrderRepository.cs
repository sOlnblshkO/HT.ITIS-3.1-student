using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetAllOrdersAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> GetOrderByGuid(Guid orderGuid, CancellationToken cancellationToken)
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