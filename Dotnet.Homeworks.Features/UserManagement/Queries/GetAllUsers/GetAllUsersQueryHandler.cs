using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Homeworks.Features.UserManagement.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IList<GetAllUsersDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(
        IUserRepository userRepository
        )
    {
        _userRepository = userRepository;
    }
    
    public async Task<Result<IList<GetAllUsersDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await (await _userRepository.GetUsersAsync()).Select( x=>
            new GetAllUsersDto(x.Id, x.Name, x.Email)
        ).ToListAsync();

        return new Result<IList<GetAllUsersDto>>(users, true);
    }
}