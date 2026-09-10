using CashOverflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashOverflow.Infrastructure.Persistence.Configurations;

public class FxRateConfiguration
    : IEntityTypeConfiguration<FxRate>
{
    public void Configure(EntityTypeBuilder<FxRate> builder)
    {
        builder.ToTable("FxRates");

        builder.HasKey(x => new
        {
            x.RateDate,
            x.BaseCurrency,
            x.QuoteCurrency
        });

        builder.Property(x => x.RateDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.BaseCurrency)
            .HasColumnType("char(3)")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.QuoteCurrency)
            .HasColumnType("char(3)")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.Rate)
            .HasPrecision(18, 6)
            .IsRequired();
    }
}