using MediatR;
using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Application.Products.Dtos;
using Ordering.Domain.Entities;

namespace Ordering.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(ProductDto Product) : IRequest<CreateProductCommandResponse>;

public record CreateProductCommandResponse(Guid Id);

public class CreateProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(Guid.NewGuid(), request.Product.ProductId, request.Product.Name, request.Product.Price);
        
        unitOfWork.Products.Add(product);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CreateProductCommandResponse(product.Id);
    }
}