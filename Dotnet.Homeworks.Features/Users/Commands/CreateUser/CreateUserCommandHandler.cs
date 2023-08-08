using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Domain.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Decorators;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Infrastructure.Utils;
using FluentValidation;

namespace Dotnet.Homeworks.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : CqrsDecorator<CreateUserCommand, Result<CreateUserDto>>, ICommandHandler<CreateUserCommand, CreateUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IEnumerable<IValidator<CreateUserCommand>> validators,
        IPermissionCheck checker,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : base(validators, checker)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<CreateUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var resultDecorators = await base.Handle(request, cancellationToken);

        if (resultDecorators.IsFailure)
            return resultDecorators;

        //TODO: маппинг
        var result = await _userRepository.InsertUser(new User()
        {
            Name = request.Name,
            Email = request.Email
        });
        await _unitOfWork.SaveChangesAsync();
        
        return new Result<CreateUserDto>(new CreateUserDto(result), true, null);
    }
}