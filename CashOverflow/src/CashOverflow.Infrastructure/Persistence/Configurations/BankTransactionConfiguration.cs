using CashOverflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashOverflow.Infrastructure.Persistence.Configurations;

public class BankTransactionConfiguration
    : IEntityTypeConfiguration<BankTransaction>
{
    public void Configure(EntityTypeBuilder<BankTransaction> builder)
    {
        builder.ToTable("BankTransactions");

        builder.HasKey(x => x.TransactionId);

        builder.Property(x => x.TransactionId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.AccountId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CompanyId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.BookingDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.ValueDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CreditDebit)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Currency)
            .HasColumnType("char(3)")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.BankTxCode)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.EndToEndId)
            .HasMaxLength(100);

        builder.Property(x => x.RemittanceInfo)
            .HasMaxLength(500);

        builder.Property(x => x.CounterpartyName)
            .HasMaxLength(200);

        builder.Property(x => x.RunningBalance)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasOne(x => x.BankAccount)
            .WithMany(x => x.BankTransactions)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.BankTransactions)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.AccountId);

        builder.HasIndex(x => x.CompanyId);

        builder.HasIndex(x => x.BookingDate);
    }
}