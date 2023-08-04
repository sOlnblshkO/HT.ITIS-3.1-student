using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

public class ProductRepositoryMock : IProductRepository
{
    private readonly Dictionary<Guid, Product> _products = new();

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var products = _products.Values;
        return Task.FromResult<IEnumerable<Product>>(products);
    }

    public Task DeleteProductByGuidAsync(Guid id)
    {
        _products.Remove(id);
        return Task.CompletedTask;
    }

    public Task UpdateProductAsync(Product product)
    {
        _products[product.Id] = product;
        return Task.CompletedTask;
    }

    public Task<Guid> InsertProductAsync(Product product)
    {
        _products.Add(product.Id, product);
        return Task.FromResult(product.Id);
    }
}