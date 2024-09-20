using MediatR;
using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Application.Exceptions;
using Ordering.Application.Products.Dtos;

namespace Ordering.Application.Products.Commands.UpdateStock;

public record UpdateStockCommand(StockUpdateDto Stock) : IRequest<UpdateStockCommandResponse>;

public record UpdateStockCommandResponse(bool IsSuccess);


public class UpdateStockCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateStockCommand, UpdateStockCommandResponse>
{
    public async Task<UpdateStockCommandResponse> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetProductByIdAsync(request.Stock.ProductId, cancellationToken);
        
        if (product is null)
            throw new NotFoundException("Product", request.Stock.ProductId);
        
        product.UpdateStock(request.Stock.Quantity);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new UpdateStockCommandResponse(true);
    }
}