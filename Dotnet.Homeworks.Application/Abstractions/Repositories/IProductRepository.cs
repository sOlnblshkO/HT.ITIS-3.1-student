using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Application.Abstractions.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    
    Task DeleteProductByGuidAsync(Guid id);
    
    Task UpdateProductAsync(Product product);
    
    Task<Guid> InsertProductAsync(Product product);
}