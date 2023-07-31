using Dotnet.Homeworks.Application.Abstractions.Repositories;
using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Infrastructure.DataAccess;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteProductByIdAsync(Guid id)
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