

namespace Basket.Basket.Models
{
    public class ShoppingCart : Aggregate<Guid>
    {
        public string UserName { get;  private set; } = default!;
        private readonly List<ShoppingCartItem> _items = new();
        public IReadOnlyCollection<ShoppingCartItem> Items => _items.AsReadOnly();
        public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity);
        public static ShoppingCart Create(Guid id, string userName)
        {
            ArgumentException.ThrowIfNullOrEmpty(userName);
            var shoppingCart = new ShoppingCart
            {
                Id = id,
                UserName = userName
            };
            return shoppingCart;
        }
        public void AddItem(Guid productId, int quantity, string color,  decimal price, string productName)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var shoppingCartItem = new ShoppingCartItem(Id, productId, productName, price, quantity, color);
                _items.Add(shoppingCartItem);
            }
        }
        public void RemoveItem(Guid productId)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                _items.Remove(existingItem);
            }
        }
    }
}
