using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Domain.Abstractions;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task DeleteProductById(int id);
    Task UpdateProduct(Product product);
    Task<int> InsertProduct(Product product);
}