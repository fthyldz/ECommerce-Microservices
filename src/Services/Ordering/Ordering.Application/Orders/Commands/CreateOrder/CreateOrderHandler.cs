using MediatR;
using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Application.Orders.Dtos;
using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto OrderDto) : IRequest<CreateOrderCommandResult>;

public record CreateOrderCommandResult(Guid Id);

public class CreateOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, CreateOrderCommandResult>
{
    public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create(Guid.NewGuid(), command.OrderDto.CustomerName, command.OrderDto.Address);
        
        unitOfWork.Orders.Add(order);
        
        foreach (var orderItemDto in command.OrderDto.OrderItems)
        {
            order.AddOrderItem(Guid.NewGuid(), order.Id, orderItemDto.ProductId, orderItemDto.Quantity, orderItemDto.Price);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateOrderCommandResult(order.Id);
    }
}