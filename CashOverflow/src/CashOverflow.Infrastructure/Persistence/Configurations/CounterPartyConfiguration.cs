using CashOverflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashOverflow.Infrastructure.Persistence.Configurations;

public class CounterPartyConfiguration
    : IEntityTypeConfiguration<CounterParty>
{
    public void Configure(EntityTypeBuilder<CounterParty> builder)
    {
        builder.ToTable("CounterParties");

        builder.HasKey(x => x.CounterpartyId);

        builder.Property(x => x.CounterpartyId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CompanyId)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.TaxId)
            .HasMaxLength(50);

        builder.Property(x => x.Role)
            .HasConversion<string>()
            .HasMaxLength(30)
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

        builder.Property(x => x.IsGovernment)
            .IsRequired();

        builder.Property(x => x.DefaultPaymentTermsDays)
            .IsRequired();

        builder.Property(x => x.RiskSegment)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.RelationshipStart)
            .HasColumnType("date")
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Counterparties)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CompanyId);
    }
}