using CashOverflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashOverflow.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration
    : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(x => x.CompanyId);

        builder.Property(x => x.CompanyId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.LegalName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.TaxId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Sector)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.SizeBand)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Governorate)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.EmployeeCount)
            .IsRequired();

        builder.Property(x => x.FoundedDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.BaseCurrency)
            .HasColumnType("char(3)")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.OpeningBalance)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.MinCashBuffer)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CreditLineLimit)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CreditLineDrawn)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IsDemo)
            .IsRequired();

        builder.HasOne(x => x.Organization)
            .WithOne(x => x.Company)
            .HasForeignKey<Company>(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.TaxId);
    }
}