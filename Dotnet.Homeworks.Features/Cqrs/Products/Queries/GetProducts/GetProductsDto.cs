namespace Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;

public record GetProductsDto(
    Guid Guid,
    string Name
    );