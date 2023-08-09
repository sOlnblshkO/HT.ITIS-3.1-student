using Dotnet.Homeworks.Domain.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Decorators;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Infrastructure.Utils;
using FluentValidation;

namespace Dotnet.Homeworks.Features.Users.Commands.DeleteProfile;

public class DeleteUserCommandHandler : CqrsDecorator<DeleteUserCommand, Result>, ICommandHandler<DeleteUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IPermissionCheck permissionCheck,
        IEnumerable<IValidator<DeleteUserCommand>> validators
    ) : base(validators, permissionCheck)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var resultDecorators = await base.Handle(request, cancellationToken);
        
        if (resultDecorators.IsFailure)
            return resultDecorators;

        //TODO: маппинг
        try
        {
            await _userRepository.DeleteUserByGuidAsync(request.Guid);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new Result(true, e.Message);
        }
        return new Result(true);
    }
}