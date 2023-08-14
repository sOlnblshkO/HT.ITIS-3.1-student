using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

public static class TestProduct
{
    public static InsertProductCommand GetInsertCommand() => new("test");
    
    public static UpdateProductCommand GetUpdateCommand(Product product) => new(product.Id, product.Name);
    
    public static DeleteProductByGuidCommand GetDeleteCommand(Guid id) => new(id);
    
    public static GetProductsQuery GetGetQuery() => new();
}