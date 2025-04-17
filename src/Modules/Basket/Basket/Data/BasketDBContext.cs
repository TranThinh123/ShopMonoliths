

using System.Reflection;
using Basket.Basket.Models;
using Microsoft.EntityFrameworkCore;

namespace Basket.Data
{
    public class BasketDBContext : DbContext
    {
        public BasketDBContext(DbContextOptions<BasketDBContext> options) : base(options)
        {
        }
        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.HasDefaultSchema("basket");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
