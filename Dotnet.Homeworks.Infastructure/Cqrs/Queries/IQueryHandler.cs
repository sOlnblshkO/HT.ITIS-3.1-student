using Dotnet.Homeworks.Infastructure.Utils;
using MediatR;

namespace Dotnet.Homeworks.Infastructure.Cqrs.Queries;


public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
    //public Task<Result<TResponse>> Handle(TQuery command, CancellationToken token);
}