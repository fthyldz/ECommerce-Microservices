using Catalog.Application.Abstractions.Persistence.Common;
using MediatR;

namespace Catalog.Application.Stocks.Commands.DecreaseStock;


public record DecreaseStockCommand(Guid ProductId, int Quantity) : IRequest<DecreaseStockCommandResult>;

public record DecreaseStockCommandResult(bool IsSuccess);

public class DecreaseStockHandlerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DecreaseStockCommand, DecreaseStockCommandResult>
{
    public async Task<DecreaseStockCommandResult> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
    {
        var stock = await unitOfWork.Stocks.GetStockByProductId(request.ProductId, cancellationToken);
        
        if (stock is null)
        {
            return new DecreaseStockCommandResult(false);
        }
        
        stock.DecreaseStock(request.Quantity);
        
        unitOfWork.Stocks.Update(stock);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new DecreaseStockCommandResult(true);
    }
}