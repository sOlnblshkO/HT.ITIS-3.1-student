using System.Collections.Concurrent;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Tests.Shared.RepositoriesMocks;

public class ProductRepositoryMock : IProductRepository
{
    private readonly ConcurrentDictionary<Guid, Product> _products = new();

    public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var products = _products.Values;
        return Task.FromResult<IEnumerable<Product>>(products);
    }

    public Task DeleteProductByGuidAsync(Guid id, CancellationToken cancellationToken)
    {
        _products.Remove(id, out var product);
        return Task.CompletedTask;
    }

    public Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        if (_products.ContainsKey(product.Id))
            _products[product.Id] = product;

        return Task.CompletedTask;
    }

    public Task<Guid> InsertProductAsync(Product product, CancellationToken cancellationToken)
    {
        _products.TryAdd(product.Id, product);
        return Task.FromResult(product.Id);
    }
}