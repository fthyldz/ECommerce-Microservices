using ECommerce.Application.Exceptions;
using MediatR;
using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Application.Products.Dtos;

namespace Ordering.Application.Products.Commands.UpdateStock;

public record UpdateStockCommand(UpdateStockDto UpdateStock) : IRequest<UpdateStockCommandResponse>;

public record UpdateStockCommandResponse(bool IsSuccess);


public class UpdateStockCommandHandler(IOrderingUnitOfWork unitOfWork) : IRequestHandler<UpdateStockCommand, UpdateStockCommandResponse>
{
    public async Task<UpdateStockCommandResponse> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetProductByIdAsync(request.UpdateStock.ProductId, cancellationToken);
        
        if (product is null)
            throw new NotFoundException("Product", request.UpdateStock.ProductId);
        
        product.UpdateStock(request.UpdateStock.Quantity);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new UpdateStockCommandResponse(true);
    }
}