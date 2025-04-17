using System;
using System.Collections.Generic;


namespace Basket.Basket.Models
{
    public class ShoppingCartItem : Entity<Guid>
    {
        public Guid ShoppingCartId { get; private set; } = default!;
        public Guid ProductId { get; private set; } = default!;


        public string ProductName { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
        public int Quantity { get; internal set; } = default!;
        public string Color { get; private set; } = default!;
        internal ShoppingCartItem(Guid shoppingCartId, Guid productId, string productName, decimal price, int quantity, string color)
        {
            ShoppingCartId = shoppingCartId;
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            Color = color;
        }

    }
}
