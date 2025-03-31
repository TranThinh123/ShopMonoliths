﻿
using Catalog.Products.Dtos;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.CQRS;

namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id);
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class CreateProductHandler(CatalogDbContext dbContext, IValidator<CreateProductCommand> validator, ILogger<CreateProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // validation part
            var result = await validator.ValidateAsync(command, cancellationToken);
            var errors = result.Errors.Select(x=>x.ErrorMessage).ToList();
            if(errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }
            // logging part
            logger.LogInformation("CreateP roductCommandHandler.Handle called with {@Command}", command);
            // actual logic
            var product = CreateNewProduct(command.Product);
            dbContext.products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);

        }

        private Product CreateNewProduct(ProductDto productDto)
        {
            var product = Product.Create(
                Guid.NewGuid(),
                productDto.Name, productDto.Category, productDto.Description,productDto.ImageFile, productDto.Price);
            return product;
        }
    }
}
