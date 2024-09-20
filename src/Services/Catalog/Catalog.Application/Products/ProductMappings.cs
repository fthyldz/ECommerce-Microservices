using Catalog.Application.Products.Dtos;
using Catalog.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Products;

public static class ProductMappings
{
    public static void RegisterProductMappings(this IServiceCollection services)
    {
        TypeAdapterConfig<Product, ProductDto>.NewConfig()
            .Map(dest => dest.Quantity, src => src.Stock.Quantity);
    }
}