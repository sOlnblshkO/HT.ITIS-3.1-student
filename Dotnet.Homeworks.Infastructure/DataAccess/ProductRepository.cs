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
    
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteProductById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<int> InsertProduct(Product product)
    {
        throw new NotImplementedException();
    }
}