using AspBackendTest.Infrastructure.Configurations;
using AspBackendTest.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace AspBackendTest.Infrastructure.Context;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }

    public DbSet<Product>? Products { get; set; }
    public DbSet<Currency>? Currencies { get; set; }
    public DbSet<PackingType>? PackingTypes { get; set; }
    public DbSet<ExchangeRate>? ExchangeRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new PackingTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ExchangeRateConfiguration());
    }
}