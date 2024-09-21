using ECommerce.Application.Exceptions;
using MediatR;
using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Application.Orders.Dtos;
using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto OrderDto) : IRequest<CreateOrderCommandResult>;

public record CreateOrderCommandResult(bool IsSuccess);

public class CreateOrderCommandHandler(IOrderingUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, CreateOrderCommandResult>
{
    public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create(Guid.NewGuid(), command.OrderDto.CustomerName, command.OrderDto.Address);
        
        order = await unitOfWork.Orders.AddAsync(order, cancellationToken);
        
        var products = await unitOfWork.Products.GetProductByIdsAsync(command.OrderDto.OrderItems.Select(x => x.ProductId).ToArray(), cancellationToken);
        
        foreach (var orderItemDto in command.OrderDto.OrderItems)
        {
            var product = products.FirstOrDefault(x => x.ProductId == orderItemDto.ProductId);
            if (product is null)
                throw new NotFoundException("Product", orderItemDto.ProductId);
            if ((product.Quantity - orderItemDto.Quantity) < 0)
                throw new Exception($"Product with id {orderItemDto.ProductId} does not have enough quantity.");
            
            order.AddOrderItem(Guid.NewGuid(), order.Id, orderItemDto.ProductId, orderItemDto.Quantity, orderItemDto.Price);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateOrderCommandResult(true);
    }
}