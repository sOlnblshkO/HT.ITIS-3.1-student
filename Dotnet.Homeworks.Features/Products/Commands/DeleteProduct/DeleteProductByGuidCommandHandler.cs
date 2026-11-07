using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductByGuidCommandHandler : ICommandHandler<DeleteProductByGuidCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductByGuidCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteProductByGuidCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteProductByGuidAsync(request.Guid, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Result(true);
    }
}
