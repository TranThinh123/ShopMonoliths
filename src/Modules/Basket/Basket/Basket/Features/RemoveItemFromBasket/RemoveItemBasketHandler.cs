

namespace Basket.Basket.Features.RemoveItemFromBasket
{
    public record RemoveItemFromBasketCommand(string UserName, Guid ProductId) : ICommand<RemoveItemFromBasketResult>;
    public record RemoveItemFromBasketResult(Guid id);
    internal class RemoveItemBasketHandler(BasketDBContext dBContext) : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = await dBContext.ShoppingCarts
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.UserName == command.UserName, cancellationToken);
            if(shoppingCart is null)
            {
                throw new BasketNotFoundException(command.UserName);
            }
            shoppingCart.RemoveItem(command.ProductId);
            await dBContext.SaveChangesAsync(cancellationToken);
            return new RemoveItemFromBasketResult(shoppingCart.Id);
        }
    }

}
