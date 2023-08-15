namespace Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;

public record GetProductDto(
    Guid Guid,
    string Name
);