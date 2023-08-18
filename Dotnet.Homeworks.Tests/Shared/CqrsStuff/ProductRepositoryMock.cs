using System.Collections.Concurrent;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Tests.Shared.CqrsStuff;

public class ProductRepositoryMock : IProductRepository
{
    private readonly ConcurrentDictionary<Guid, Product> _products = new();

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var products = _products.Values;
        return Task.FromResult<IEnumerable<Product>>(products);
    }

    public Task DeleteProductByGuidAsync(Guid id)
    {
        _products.Remove(id, out var product);
        return Task.CompletedTask;
    }

    public Task UpdateProductAsync(Product product)
    {
        if (_products.ContainsKey(product.Id))
            _products[product.Id] = product;

        return Task.CompletedTask;
    }

    public Task<Guid> InsertProductAsync(Product product)
    {
        _products.TryAdd(product.Id, product);
        return Task.FromResult(product.Id);
    }
}