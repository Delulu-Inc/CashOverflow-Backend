using CashOverflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashOverflow.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration
    : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(x => x.Uuid);

        builder.Property(x => x.Uuid)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.InternalId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CompanyId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CounterpartyId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Direction)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.DocumentType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.IssueDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.DueDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.PaymentTermsDays)
            .IsRequired();

        builder.Property(x => x.Currency)
            .HasColumnType("char(3)")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.FxRateToBase)
            .HasPrecision(18, 6)
            .IsRequired();

        builder.Property(x => x.NetAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.DiscountAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.VatAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.RetentionPct)
            .HasPrecision(9, 4)
            .IsRequired();

        builder.Property(x => x.RetentionAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.ReferenceUuid)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Invoices)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Counterparty)
            .WithMany(x => x.Invoices)
            .HasForeignKey(x => x.CounterpartyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.CompanyId);

        builder.HasIndex(x => x.CounterpartyId);

        builder.HasIndex(x => x.DueDate);

        builder.HasIndex(x => x.Status);
    }
}