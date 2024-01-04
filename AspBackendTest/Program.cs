using AspBackendTest.Application.IRepositories;
using AspBackendTest.Application.Seed;
using AspBackendTest.Application.UseCase.Currency;
using AspBackendTest.Application.UseCase.ExchangeRate;
using AspBackendTest.Application.UseCase.PackingType;
using AspBackendTest.Application.UseCase.Product;
using AspBackendTest.Extensions;
using AspBackendTest.Infrastructure.Context;
using AspBackendTest.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<ProductContext>(options => options.UseSqlite(configuration.GetConnectionString("sqlite")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IPackingTypeRepository, PackingTypeRepository>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

builder.Services.AddScoped<GetAllProductUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();
builder.Services.AddScoped<GetAllCurrencyUseCase>();
builder.Services.AddScoped<UpdateCurrencyUseCase>();
builder.Services.AddScoped<CreateCurrencyUseCase>();
builder.Services.AddScoped<DeleteCurrencyUseCase>();
builder.Services.AddScoped<GetAllPackingTypeUseCase>();
builder.Services.AddScoped<UpdatePackingTypeUseCase>();
builder.Services.AddScoped<CreatePackingTypeUseCase>();
builder.Services.AddScoped<DeletePackingTypeUseCase>();
builder.Services.AddScoped<GetAllExchangeRateUseCase>();
builder.Services.AddScoped<GetLastRateUseCase>();
builder.Services.AddScoped<CreateExchangeRateUseCase>();
builder.Services.AddScoped<DeleteExchangeRateUseCase>();
builder.Services.AddScoped<DefaultSeed>();


builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddSwagger();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var defaultSeed = scope.ServiceProvider.GetService<DefaultSeed>();
    defaultSeed!.Seed();
}

app.UseSwagger();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

app.MapControllers();

app.Run();