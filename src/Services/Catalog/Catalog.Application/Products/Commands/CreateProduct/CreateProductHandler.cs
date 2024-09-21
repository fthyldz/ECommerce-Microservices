using Catalog.Application.Abstractions.Persistence.Common;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Products.Commands.CreateProduct;

public record CreateProductDto(string Name, string Description, decimal Price, int Quantity);

public record CreateProductCommand(CreateProductDto Product) : IRequest<CreateProductCommandResult>;

public record CreateProductCommandResult(Guid Id);

public class CreateProductCommandHandler(ICatalogUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, CreateProductCommandResult>
{
    public async Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productId = Guid.NewGuid();
        var product = Product.Create(productId, request.Product.Name, request.Product.Description, request.Product.Price, Stock.Create(Guid.NewGuid(), productId, request.Product.Quantity));

        await unitOfWork.Products.AddAsync(product, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateProductCommandResult(product.Id);
    }
}