using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Commands.InsertProduct;

internal sealed class InsertProductCommandHandler : ICommandHandler<InsertProductCommand, InsertProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InsertProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<InsertProductDto>> Handle(InsertProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product { Name = request.Name };

        var guid = await _productRepository.InsertProductAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Result<InsertProductDto>(new InsertProductDto(guid), true);
    }
}
