using MediatR;

namespace ECommerce.Application.Abstractions.Application.Cqrs;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}