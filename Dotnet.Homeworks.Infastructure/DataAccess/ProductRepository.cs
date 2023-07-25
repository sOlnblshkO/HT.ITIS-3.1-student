using Dotnet.Homeworks.Domain.Abstractions;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Infastructure.DatabaseContext;

namespace Dotnet.Homeworks.Infastructure.RepositoryImplementations;

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

    public async Task DeleteProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<int> InsertProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}