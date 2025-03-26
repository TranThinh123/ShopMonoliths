using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<Product> products => new List<Product>
       {
    Product.Create(new Guid("123e4567-e89b-12d3-a456-426614174000"), "Product 1", ["Description 1"], "long", "100", 100),
    Product.Create(new Guid("123e4567-e89b-12d3-a456-426614174001"), "Product 2", ["Description 2"], "long1", "10022", 100),
    Product.Create(new Guid("123e4567-e89b-12d3-a456-426614174002"), "Product 3", ["Description 3"], "long2", "1002", 100),
    Product.Create(new Guid("123e4567-e89b-12d3-a456-426614174003"), "Product 4", ["Description 4"], "long3", "1001", 100)
};
    }
}
