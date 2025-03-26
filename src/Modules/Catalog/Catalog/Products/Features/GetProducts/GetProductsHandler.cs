using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Products.Dtos;
using Mapster;
using Shared.CQRS;

namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<ProductDto> Products);
    public class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
           var products = await dbContext.products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);
            var productDtos = products.Adapt<List<ProductDto>>();
            return new GetProductsResult(productDtos);
        }

        
    }
}
