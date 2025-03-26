﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Seed;

namespace Shared.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UserMigration<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            MigrationDatabaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();
            SeedDataAsync(app.ApplicationServices).GetAwaiter().GetResult();
            return app;
        }

       

        private static async Task MigrationDatabaseAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            await context.Database.MigrateAsync();
        }
        private static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
            foreach(var seeder in seeders)
            {
                await seeder.SeedAllAsync();
            }
        }

    }
}
