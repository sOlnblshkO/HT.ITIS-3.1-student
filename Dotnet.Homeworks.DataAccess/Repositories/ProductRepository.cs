using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteProductByGuidAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> InsertProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}