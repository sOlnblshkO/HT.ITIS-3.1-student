using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductByGuidAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> InsertProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}