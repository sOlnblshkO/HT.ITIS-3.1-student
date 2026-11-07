using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<GetProductsDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsAsync(cancellationToken);

        // Query ничего не меняет, поэтому IUnitOfWork здесь не нужен.
        // Mapping пока не используется (по условию), поэтому маппим вручную.
        var dto = new GetProductsDto(products.Select(p => new GetProductDto(p.Id, p.Name)).ToList());

        return new Result<GetProductsDto>(dto, true);
    }
}
