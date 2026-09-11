using CashOverflow.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CashOverflow.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
    }
    public DbSet<Organization> Organizations => Set<Organization>();

    public DbSet<OrganizationMember> OrganizationMembers => Set<OrganizationMember>();

    public DbSet<Invitation> Invitations => Set<Invitation>();

    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public DbSet<Payment> Payments => Set<Payment>();

    public DbSet<Company> Companies => Set<Company>();

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    public DbSet<BankTransaction> BankTransactions => Set<BankTransaction>();

    public DbSet<CounterParty> CounterParties => Set<CounterParty>();

    public DbSet<Invoice> Invoices => Set<Invoice>();

    public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();

    public DbSet<Settlement> Settlements => Set<Settlement>();

    public DbSet<RecurringObligation> RecurringObligations => Set<RecurringObligation>();

    public DbSet<FxRate> FxRates => Set<FxRate>();

    public DbSet<DemoRequest> DemoRequests => Set<DemoRequest>();

}