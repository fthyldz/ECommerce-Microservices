using Catalog.Application.Abstractions.Persistence.Common;
using MediatR;

namespace Catalog.Application.Stocks.Commands.DecreaseStock;

public record DecreaseStockRequestDto(Guid ProductId, int Quantity);

public record DecreaseStockCommand(DecreaseStockRequestDto DecreaseStockRequestDto) : IRequest<DecreaseStockCommandResult>;

public record DecreaseStockCommandResult(bool IsSuccess);

public class DecreaseStockHandlerCommandHandler(ICatalogUnitOfWork unitOfWork) : IRequestHandler<DecreaseStockCommand, DecreaseStockCommandResult>
{
    public async Task<DecreaseStockCommandResult> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetProductById(request.DecreaseStockRequestDto.ProductId, cancellationToken);
        
        if (product is null)
        {
            throw new Exception($"Product with id {request.DecreaseStockRequestDto.ProductId} not found");
        }
        
        product.DecreaseStock(request.DecreaseStockRequestDto.Quantity);
        
        unitOfWork.Products.Update(product);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new DecreaseStockCommandResult(true);
    }
}