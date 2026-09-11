using CashOverflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashOverflow.Infrastructure.Persistence.Configurations;

public class SettlementConfiguration
    : IEntityTypeConfiguration<Settlement>
{
    public void Configure(EntityTypeBuilder<Settlement> builder)
    {
        builder.ToTable("Settlements");

        builder.HasKey(x => x.SettlementId);

        builder.Property(x => x.SettlementId)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.InvoiceUuid)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.CompanyId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PaidDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.PaidAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Currency)
            .HasColumnType("char(3)")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.Method)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.SequenceNo)
            .IsRequired();

        builder.Property(x => x.IsFinal)
            .IsRequired();

        builder.Property(x => x.BankTransactionId)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.HasOne(x => x.Invoice)
            .WithMany(x => x.Settlements)
            .HasForeignKey(x => x.InvoiceUuid)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Settlements)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.BankTransaction)
            .WithMany(x => x.Settlements)
            .HasForeignKey(x => x.BankTransactionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.InvoiceUuid);

        builder.HasIndex(x => x.CompanyId);

        builder.HasIndex(x => x.BankTransactionId);
    }
}