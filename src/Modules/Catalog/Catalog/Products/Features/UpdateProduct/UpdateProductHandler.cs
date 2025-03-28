﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Products.Dtos;
using Shared.CQRS;

namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDto Product ) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {

            var product = await dbContext.products.FindAsync([command.Product.Id], cancellationToken : cancellationToken);
            if (product == null)
            {
                throw new Exception($"Product not found: {command.Product.Id}");
            }
            UpdateProductWithNewValues(product, command.Product);
            dbContext.products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }

        private void UpdateProductWithNewValues(Product product, ProductDto productDto)
        {
            product.Update(
                productDto.Name, productDto.Category, productDto.Description, productDto.ImageFile, productDto.Price);
        }
    }
}
