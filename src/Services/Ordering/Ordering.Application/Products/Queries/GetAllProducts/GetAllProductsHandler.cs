using Mapster;
using MediatR;
using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Application.Products.Dtos;

namespace Ordering.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;

public class GetAllProductsQueryHandler(IOrderingUnitOfWork unitOfWork) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.Products.FindAllAsync(null, cancellationToken);

        var productDtos = products.Adapt<IEnumerable<ProductDto>>();

        return productDtos;
    }
}