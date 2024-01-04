using Microsoft.EntityFrameworkCore;
using AspBackendTest.Infrastructure.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspBackendTest.Infrastructure.Configurations;

public sealed class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(currency => currency.Id);
        builder.HasQueryFilter(currency => !currency.IsDeleted);

        builder.Property(currency => currency.Name).IsRequired();
        builder.Property(currency => currency.EnglishName).IsRequired();
        builder.Property(currency => currency.Code).IsRequired();

        builder.Property(currency => currency.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(currency => currency.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
    }
}