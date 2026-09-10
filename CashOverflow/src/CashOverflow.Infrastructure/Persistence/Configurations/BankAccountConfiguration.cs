using CashOverflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashOverflow.Infrastructure.Persistence.Configurations;

public class BankAccountConfiguration
    : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("BankAccounts");

        builder.HasKey(x => x.AccountId);

        builder.Property(x => x.AccountId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CompanyId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Iban)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.BankName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Currency)
            .HasColumnType("char(3)")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.OpeningBalance)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.OpenedDate)
            .HasColumnType("date")
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany(x => x.BankAccounts)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CompanyId);

        builder.HasIndex(x => x.Iban)
            .IsUnique();
    }
}