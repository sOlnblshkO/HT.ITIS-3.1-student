using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return _context.Products.ToList();
    }

    public async Task DeleteProductByGuidAsync(Guid id)
    {
         _context.Products.Remove(new Product() {Id = id});
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
    }

    public async Task<Guid> InsertProductAsync(Product product)
    {
        var entity = await _context.Products.AddAsync(product);
        return entity.Entity.Id;
    }
}