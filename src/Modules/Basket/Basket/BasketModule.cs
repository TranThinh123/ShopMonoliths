

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Interceptors;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container
            // SERVICES
            // . AddAppllicationServices()
            // . AddInfrastructureServices(configuration)
            // . AddApiServices(configuration)
            var connectionString = configuration.GetConnectionString("Database");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.AddDbContext<BasketDBContext>((sp, options) =>
            {
              
                options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            return services;
        }
        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {
            // configure the HTTP request pipeline
            // app
            // .UseCatalogModule()
            app.UserMigration<BasketDBContext>();
            return app;
        }
    }
}
