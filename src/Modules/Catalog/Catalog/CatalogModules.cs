
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog
{
    public static class CatalogModules
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container
            // SERVICES
            // . AddAppllicationServices()
            // . AddInfrastructureServices(configuration)
            // . AddApiServices(configuration)
            return services;
        }
        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            // configure the HTTP request pipeline
            // app
            // .UseCatalogModule()
            return app;
        }
    }
}
