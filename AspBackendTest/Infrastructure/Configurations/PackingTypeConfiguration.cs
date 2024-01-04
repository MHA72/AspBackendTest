using Microsoft.EntityFrameworkCore;
using AspBackendTest.Infrastructure.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspBackendTest.Infrastructure.Configurations;

public sealed class PackingTypeConfiguration : IEntityTypeConfiguration<PackingType>
{
    public void Configure(EntityTypeBuilder<PackingType> builder)
    {
        builder.HasKey(type => type.Id);
        builder.HasQueryFilter(type => !type.IsDeleted);

        builder.Property(type => type.Price).IsRequired();
        builder.Property(type => type.Name).IsRequired();

        builder.Property(type => type.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(type => type.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
    }
}