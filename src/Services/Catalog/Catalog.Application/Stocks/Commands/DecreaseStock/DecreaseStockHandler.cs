using Catalog.Application.Abstractions.Persistence.Common;
using MediatR;

namespace Catalog.Application.Stocks.Commands.DecreaseStock;


public record DecreaseStockCommand(Guid ProductId, int Quantity, Guid CorrelationId) : IRequest<DecreaseStockCommandResult>;

public record DecreaseStockCommandResult(bool IsSuccess);

public class DecreaseStockHandlerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DecreaseStockCommand, DecreaseStockCommandResult>
{
    public async Task<DecreaseStockCommandResult> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetProductById(request.ProductId, cancellationToken);
        
        if (product is null)
        {
            throw new Exception($"Product with id {request.ProductId} not found");
        }
        
        product.DecreaseStock(request.Quantity, request.CorrelationId);
        
        unitOfWork.Products.Update(product);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new DecreaseStockCommandResult(true);
    }
}