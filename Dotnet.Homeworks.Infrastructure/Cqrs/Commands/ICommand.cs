using Dotnet.Homeworks.Shared.Dto;
using MediatR;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}