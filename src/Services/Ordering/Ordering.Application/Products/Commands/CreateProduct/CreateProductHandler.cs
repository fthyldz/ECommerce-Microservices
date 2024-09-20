using MediatR;
using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Application.Products.Dtos;
using Ordering.Domain.Entities;

namespace Ordering.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(CreateProductDto Product) : IRequest<CreateProductCommandResponse>;

public record CreateProductCommandResponse(bool IsSuccess);

public class CreateProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(Guid.NewGuid(), request.Product.ProductId, request.Product.Name, request.Product.Price, request.Product.Quantity);
        
        unitOfWork.Products.Add(product);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CreateProductCommandResponse(true);
    }
}