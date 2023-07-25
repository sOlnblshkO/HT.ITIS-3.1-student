using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Domain.Abstractions;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    
    Task DeleteProductByIdAsync(int id);
    
    Task UpdateProductAsync(Product product);
    
    Task<int> InsertProductAsync(Product product);
}