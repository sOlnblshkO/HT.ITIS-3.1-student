using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Decorators;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Utils;
using FluentValidation;

namespace Dotnet.Homeworks.Features.Users.Queries.GetUser;

public class GetUserQueryHandler : CqrsDecorator<GetUserQuery, Result<GetUserDto>>, IQueryHandler<GetUserQuery, GetUserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(
        IUserRepository userRepository, 
        IEnumerable<IValidator<GetUserQuery>> validators,
        IPermissionCheck checkers
    )
    : base(validators, checkers)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByGuid(request.Guid);
        var result = new GetUserDto(user.Id, user.Name, user.Email);
        return new Result<GetUserDto>(result, true, null);
    }
}