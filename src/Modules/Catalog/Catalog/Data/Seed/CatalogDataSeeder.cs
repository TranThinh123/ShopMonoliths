using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Data.Seed;

namespace Catalog.Data.Seed
{
    public class CatalogDataSeeder(CatalogDbContext dbContext) : IDataSeeder
    {
        public async Task SeedAllAsync()
        {
            if(!await dbContext.products.AnyAsync())
            {
                await dbContext.products.AddRangeAsync(InitialData.products);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
