
namespace Basket.Basket.Features.AddItemIntoBasket
{
    public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItemDto) : ICommand<AddItemIntoBasketResult>;
    public record AddItemIntoBasketResult(Guid id);
    public class AddItemIntoBasketCommandValidator : AbstractValidator<AddItemIntoBasketCommand>
    {
        public AddItemIntoBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.ShoppingCartItemDto.ProductId).NotEmpty().WithMessage("ProductId is required");
            RuleFor(x => x.ShoppingCartItemDto.Quantity).GreaterThan(0).WithMessage("Quantity must be");
        }
    }
    internal class AddItemIntoBasketHandler(BasketDBContext dBContext) : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = await dBContext.ShoppingCarts.SingleOrDefaultAsync(x => x.UserName == command.UserName, cancellationToken);
            if (shoppingCart is null)
            {
                throw new BasketNotFoundException(command.UserName);
            }
            shoppingCart.AddItem(
                command.ShoppingCartItemDto.ProductId,
                command.ShoppingCartItemDto.Quantity,
                command.ShoppingCartItemDto.Color,
                command.ShoppingCartItemDto.Price,
                command.ShoppingCartItemDto.ProductName);
            await dBContext.SaveChangesAsync(cancellationToken);
            return new AddItemIntoBasketResult(shoppingCart.Id);
        }
    }
    
}
