namespace Dotnet.Homeworks.Features.Products.Queries.GetProducts;

public record GetProductsDto(
    IEnumerable<GetProductDto> Products
    );