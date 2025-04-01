using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Products.Dtos;
using Catalog.Products.Exceptions;
using Mapster;
using Shared.CQRS;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(ProductDto Product);
    internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await dbContext.products
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(query.Id);
            }
            var productDto = product.Adapt<ProductDto>();
            return new GetProductByIdResult(productDto);
        }
    }
 
}
