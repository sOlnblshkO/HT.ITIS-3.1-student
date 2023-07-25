using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Domain.Abstractions;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    
    Task DeleteProductByIdAsync(int id);
    
    Task UpdateProductAsync(Product product);
    
    /// <summary>
    /// Returns id of inserted
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<int> InsertProductAsync(Product product);
}