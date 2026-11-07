using Dotnet.Homeworks.Shared.Dto;
using MediatR;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}