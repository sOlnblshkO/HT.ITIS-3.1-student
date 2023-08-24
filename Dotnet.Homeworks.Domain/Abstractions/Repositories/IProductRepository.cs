using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Domain.Abstractions.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
    
    Task DeleteProductByGuidAsync(Guid guid, CancellationToken cancellationToken);
    
    Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
    
    Task<Guid> InsertProductAsync(Product product, CancellationToken cancellationToken);
}