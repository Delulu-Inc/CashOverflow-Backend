using CashOverflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashOverflow.Infrastructure.Persistence.Configurations;

public class InvoiceLineConfiguration
    : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.ToTable("InvoiceLines");

        builder.HasKey(x => x.LineId);

        builder.Property(x => x.LineId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.InvoiceUuid)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.LineNumber)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.ItemCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.UnitType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.DiscountAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.NetAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.VatRate)
            .HasPrecision(9, 4)
            .IsRequired();

        builder.Property(x => x.VatAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasOne(x => x.Invoice)
            .WithMany(x => x.InvoiceLines)
            .HasForeignKey(x => x.InvoiceUuid)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.InvoiceUuid);

        builder.HasIndex(x => new
        {
            x.InvoiceUuid,
            x.LineNumber
        }).IsUnique();
    }
}