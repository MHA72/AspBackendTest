using Microsoft.EntityFrameworkCore;
using AspBackendTest.Infrastructure.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspBackendTest.Infrastructure.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.HasQueryFilter(product => !product.IsDeleted);

        builder.Property(product => product.PriceCurrency).IsRequired();
        builder.Property(product => product.TotalPrice).IsRequired();
        builder.Property(product => product.Name).IsRequired();
        builder.Property(product => product.FilePath).IsRequired();

        builder.Property(product => product.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(product => product.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());

        builder.HasOne(product => product.Currency)
            .WithMany()
            .HasForeignKey(product => product.CurrencyId)
            .IsRequired();

        builder.HasOne(product => product.PackingType)
            .WithMany()
            .HasForeignKey(product => product.PackingTypeId)
            .IsRequired();
    }
}