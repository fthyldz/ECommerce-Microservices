using MediatR;

namespace ECommerce.Application.Abstractions.Application.Cqrs;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}