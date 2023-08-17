using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Tests.Shared.CqrsStuff;

public static class TestProduct
{
    private static InsertProductCommand GetInsertCommand() => new("test");
    
    public static UpdateProductCommand GetUpdateCommand(Product product) => new(product.Id, product.Name);
    
    public static DeleteProductByGuidCommand GetDeleteCommand(Guid id) => new(id);
    
    public static GetProductsQuery GetGetQuery() => new();
    
    public static async Task<Result<InsertProductDto>> CreateProductAsync(IMediator mediator)
    {
        var createProduct = GetInsertCommand();
        return await mediator.Send(createProduct);
    }
}