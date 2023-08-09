using Dotnet.Homeworks.Domain.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Features.UserManagement.Commands.DeleteUserByAdmin;

public class DeleteUserByAdminCommandHandler : ICommandHandler<DeleteUserByAdminCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public DeleteUserByAdminCommandHandler (
        IUnitOfWork unitOfWork,
        IUserRepository userRepository
        )
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(DeleteUserByAdminCommand request, CancellationToken cancellationToken)
    {
        //TODO: маппинг
        try
        {
            await _userRepository.DeleteUserByGuidAsync(request.Guid);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
        return new Result(true);
    }
}