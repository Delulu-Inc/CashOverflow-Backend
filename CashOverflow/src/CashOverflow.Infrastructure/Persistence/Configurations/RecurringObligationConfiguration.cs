using CashOverflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Infrastructure.Persistence.Configurations
{
    public class RecurringObligationConfiguration : IEntityTypeConfiguration<RecurringObligation>
    {
        public void Configure(EntityTypeBuilder<RecurringObligation> builder)
        {
          
            builder.HasKey(r => r.ObligationId);
            builder.Property(r => r.ObligationId)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(r => r.CompanyId)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(r => r.Kind)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(r => r.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.Currency)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(r => r.Frequency)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.DayOfMonth)
                .IsRequired();

            builder.Property(r => r.BusinessDayRule)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.VariabilityPct)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.EndDate)
                .IsRequired(false);
           
            builder.HasOne(r => r.Company)
                .WithMany(c => c.RecurringObligations)
                .HasForeignKey(r => r.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
