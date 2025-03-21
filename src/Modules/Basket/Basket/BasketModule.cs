

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            return services;
        }
        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {
            // configure the HTTP request pipeline
            // app
            // .UseCatalogModule()
            return app;
        }
    }
}
