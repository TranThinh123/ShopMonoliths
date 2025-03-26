using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Products.Dtos;
using Shared.CQRS;

namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    internal class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.products.FindAsync([command.ProductId], cancellationToken: cancellationToken);    
            if(product == null)
            {
                throw new Exception($"Product not found: {command.ProductId}");
            }
            dbContext.products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
