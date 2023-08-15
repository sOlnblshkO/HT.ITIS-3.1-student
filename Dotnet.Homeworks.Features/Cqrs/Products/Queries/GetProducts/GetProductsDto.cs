namespace Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;

public record GetProductsDto(
    IEnumerable<GetProductDto> Products
    );