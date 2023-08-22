using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Tests.Shared.CqrsStuff;

public static class TestProduct
{
    public static async Task<Result<InsertProductDto>> CreateProductAsync(IMediator mediator)
    {
        var createProduct = new InsertProductCommand("test");
        return await mediator.Send(createProduct);
    }

    public static async Task<Result<GetProductsDto>> GetProductsAsync(IMediator mediator)
    {
        var getProducts = new GetProductsQuery();
        return await mediator.Send(getProducts);
    }

    public static async Task<dynamic> CreateProductAsync(MediatR.IMediator mediatR)
    {
        var createProduct = new InsertProductCommand("test");
        return await mediatR.Send(createProduct);
    }

    public static async Task<dynamic> GetProductsAsync(MediatR.IMediator mediatR)
    {
        var getProducts = new GetProductsQuery();
        return await mediatR.Send(getProducts);
    }
}