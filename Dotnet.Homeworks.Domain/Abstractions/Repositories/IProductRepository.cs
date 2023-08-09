using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Domain.Abstractions.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    
    Task DeleteProductByGuidAsync(Guid guid);
    
    Task UpdateProductAsync(Product product);
    
    Task<Guid> InsertProductAsync(Product product);
}