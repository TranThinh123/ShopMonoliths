using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Products.Dtos;
using Mapster;
using Shared.CQRS;
using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;
    public record GetProductsResult(PaginatedResult<ProductDto> Products);
    public class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount = await dbContext.products.LongCountAsync(cancellationToken);
            var products = await dbContext.products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            var productDtos = products.Adapt<List<ProductDto>>();
            return new GetProductsResult(
                new PaginatedResult<ProductDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    productDtos
                    ));
        }

        
    }
}
