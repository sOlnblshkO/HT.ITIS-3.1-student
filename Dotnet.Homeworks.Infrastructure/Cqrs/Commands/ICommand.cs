using Dotnet.Homeworks.Infrastructure.Cqrs.Utils;
using MediatR;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

public interface ICommand : IRequest<Result>
{
    public Result Result { get; set; }
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
    public Result<TResponse> Result { get; set; }
}