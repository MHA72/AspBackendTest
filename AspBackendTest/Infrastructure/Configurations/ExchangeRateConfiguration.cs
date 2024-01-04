using Microsoft.EntityFrameworkCore;
using AspBackendTest.Infrastructure.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspBackendTest.Infrastructure.Configurations;

public sealed class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(exchangeRate => exchangeRate.Id);
        builder.HasQueryFilter(exchangeRate => !exchangeRate.IsDeleted);

        builder.Property(exchangeRate => exchangeRate.EffectiveDate).IsRequired();

        builder.Property(exchangeRate => exchangeRate.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(exchangeRate => exchangeRate.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(exchangeRate => exchangeRate.EffectiveDate)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());

        builder.HasOne(exchangeRate => exchangeRate.FromCurrency)
            .WithMany()
            .HasForeignKey(exchangeRate => exchangeRate.FromCurrencyId)
            .IsRequired();

        builder.HasOne(exchangeRate => exchangeRate.ToCurrency)
            .WithMany()
            .HasForeignKey(exchangeRate => exchangeRate.ToCurrencyId)
            .IsRequired();
    }
}