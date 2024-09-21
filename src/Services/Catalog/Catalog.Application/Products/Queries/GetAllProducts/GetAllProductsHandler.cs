using Catalog.Application.Abstractions.Persistence.Common;
using Catalog.Application.Products.Dtos;
using Mapster;
using MediatR;

namespace Catalog.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;

public class GetAllProductsQueryHandler(ICatalogUnitOfWork unitOfWork) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.Products.GetAllProducts(cancellationToken);

        var productDtos = products.Adapt<IEnumerable<ProductDto>>();

        return productDtos;
    }
}