
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCarterWithAssemblies(typeof(CatalogModule).Assembly);
// add services to the container
//builder.Services.AddCarter(configurator: config =>
//{
//    var catalogModule = typeof(CatalogModule).Assembly.GetTypes()
//    .Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();
//    config.WithModules(catalogModule);
//});
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services
    .AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

// configure the HTTP request pipeline
app.MapCarter();
app
    .UseCatalogModule()
    .UseBasketModule() 
    .UseOrderingModule();
app.UseExceptionHandler(options => { });
app.Run();
