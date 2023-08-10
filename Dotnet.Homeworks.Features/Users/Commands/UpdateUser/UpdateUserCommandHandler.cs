using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Decorators;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Infrastructure.Utils;
using FluentValidation;

namespace Dotnet.Homeworks.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : CqrsDecorator<UpdateUserCommand, Result>, ICommandHandler<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IPermissionCheck permissionCheck,
        IEnumerable<IValidator<UpdateUserCommand>> validators
        ) : base(validators, permissionCheck)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var resultDecorators = await base.Handle(request, cancellationToken);
        
        if (resultDecorators.IsFailure)
            return resultDecorators;

        //TODO: маппинг
        try
        {
            await _userRepository.UpdateUserAsync(request.User);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
        return new Result(true);
    }
}