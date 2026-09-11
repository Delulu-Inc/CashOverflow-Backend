using CashOverflow.Domain.Entities;
using CashOverflow.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using CashOverflow.Application.Common.Helpers;
using System.Text;

namespace CashOverflow.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(
      ApplicationDbContext context,
      UserManager<ApplicationUser> userManager,
      RoleManager<IdentityRole> roleManager)
        {
            // 1. Automatically run EF Core Migrations on startup
            await context.Database.MigrateAsync();

            // 2. Seed Default Identity Roles ("Owner", "FinanceManager")
            string[] roles = ["Owner", "FinanceManager"];

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpperInvariant()
                    });
                }
            }
            // 3. Seed Default Organization, User & Subscription (Independent of CSV seeding)
            var existingUser = await userManager.FindByEmailAsync("ahmed@nileconstruction.eg");

            if (existingUser == null)
            {
                var demoOrg = await context.Organizations.FindAsync("DEMO");


                if (demoOrg == null)
                {
                    demoOrg = new Organization
                    {
                        Id = "DEMO",
                        Name = "Al Nile Contracting and Supplies"
                    };
                    await context.Organizations.AddAsync(demoOrg);
                    await context.SaveChangesAsync();
                }


                var defaultUser = new ApplicationUser
                {
                    UserName = "ahmed@nileconstruction.eg",
                    Email = "ahmed@nileconstruction.eg",
                    FirstName = "Ahmed",
                    LastName = "Mohamed",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(defaultUser, "DemoPass123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, "Owner");
                    var membership = new OrganizationMember
                    {
                        Id = Guid.NewGuid(),
                        Name = $"{defaultUser.FirstName} {defaultUser.LastName}",
                        ApplicationUserId = defaultUser.Id,
                        OrganizationId = demoOrg.Id
                    };
                    await context.OrganizationMembers.AddAsync(membership);
                    var subscription = new Subscription
                    {
                        Id = Guid.NewGuid(),
                        OrganizationId = demoOrg.Id,
                        SubscriptionPlan = "Enterprise",
                        Status = SubscriptionStatus.Active,
                        StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                        EndDate = new DateOnly(2099, 12, 31)
                    };


                    await context.Subscriptions.AddAsync(subscription);
                    await context.SaveChangesAsync();
                    Console.WriteLine("[DbInitializer] Default user ahmed@nileconstruction.eg successfully created.");
                }

                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    Console.WriteLine($"[DbInitializer ERROR] Failed to create default user: {errors}");
                }
            }

            // 4. Seed Financial Data from SeedData/*.csv files
            await SeedCsvFilesAsync(context);
        }

        private static async Task SeedCsvFilesAsync(ApplicationDbContext context)
        {
            var possibleFolders = new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData"),
                Path.Combine(Directory.GetCurrentDirectory(), "SeedData"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "CashOverflow.Infrastructure", "SeedData"),
                @"D:\Downloads\yarab victoris\backend-handoff\seed-data"
            };

            var seedFolder = possibleFolders.FirstOrDefault(Directory.Exists);

            if (string.IsNullOrEmpty(seedFolder)) return;

            var culture = CultureInfo.InvariantCulture;

            // Seed Company
            var companyPath = Path.Combine(seedFolder, "company.csv");

            if (File.Exists(companyPath) && !await context.Companies.AnyAsync())
            {
                var lines = await File.ReadAllLinesAsync(companyPath);
                foreach (var line in lines.Skip(1))
                {
                    var cols = SeedingHelpers.ParseCsvLine(line);
                    if (cols.Length < 14) continue;
                    context.Companies.Add(new Company
                    {
                        CompanyId = cols[0],
                        LegalName = cols[1],
                        TaxId = cols[2],
                        Sector = cols[3],
                        SizeBand = cols[4],
                        Governorate = cols[5],
                        EmployeeCount = int.Parse(cols[6], culture),
                        FoundedDate = DateOnly.Parse(cols[7], culture),
                        BaseCurrency = cols[8],
                        OpeningBalance = decimal.Parse(cols[9], culture),
                        MinCashBuffer = decimal.Parse(cols[10], culture),
                        CreditLineLimit = decimal.Parse(cols[11], culture),
                        CreditLineDrawn = decimal.Parse(cols[12], culture),
                        IsDemo = bool.Parse(cols[13])
                    });
                }
                await context.SaveChangesAsync();
            }

            // Seed CounterParties
            var counterpartyPath = Path.Combine(seedFolder, "counterparty.csv");

            if (File.Exists(counterpartyPath) && !await context.CounterParties.AnyAsync())
            {
                var lines = await File.ReadAllLinesAsync(counterpartyPath);
                foreach (var line in lines.Skip(1))
                {
                    var cols = SeedingHelpers.ParseCsvLine(line);
                    if (cols.Length < 12) continue;
                    context.CounterParties.Add(new CounterParty
                    {
                        CounterpartyId = cols[0],
                        CompanyId = cols[1],
                        Name = cols[2],
                        TaxId = string.IsNullOrWhiteSpace(cols[3]) ? null : cols[3],
                        Role = SeedingHelpers.ParseCounterpartyRole(cols[4]),
                        Sector = cols[5],
                        SizeBand = cols[6],
                        Governorate = cols[7],
                        IsGovernment = bool.Parse(cols[8]),
                        DefaultPaymentTermsDays = int.Parse(cols[9], culture),
                        RiskSegment = cols[10],
                        RelationshipStart = DateOnly.Parse(cols[11], culture)
                    });
                }

                await context.SaveChangesAsync();
            }
            // Seed Bank Accounts
            var accountPath = Path.Combine(seedFolder, "bank_account.csv");

            if (File.Exists(accountPath) && !await context.BankAccounts.AnyAsync())
            {
                var lines = await File.ReadAllLinesAsync(accountPath);
                foreach (var line in lines.Skip(1))
                {
                    var cols = SeedingHelpers.ParseCsvLine(line);
                    if (cols.Length < 7) continue;
                    context.BankAccounts.Add(new BankAccount
                    {
                        AccountId = cols[0],
                        CompanyId = cols[1],
                        Iban = cols[2],
                        BankName = cols[3],
                        Currency = cols[4],
                        OpeningBalance = decimal.Parse(cols[5], culture),
                        OpenedDate = DateOnly.Parse(cols[6], culture)
                    });
                }
                await context.SaveChangesAsync();
            }
            // Seed Invoices
            var invoicePath = Path.Combine(seedFolder, "invoice.csv");

            if (File.Exists(invoicePath) && !await context.Invoices.AnyAsync())
            {
                var lines = await File.ReadAllLinesAsync(invoicePath);
                foreach (var line in lines.Skip(1))
                {
                    var cols = SeedingHelpers.ParseCsvLine(line);
                    if (cols.Length < 19) continue;
                    context.Invoices.Add(new Invoice
                    {
                        Uuid = cols[0],
                        InternalId = cols[1],
                        CompanyId = cols[2],
                        CounterpartyId = cols[3],
                        Direction = SeedingHelpers.ParseInvoiceDirection(cols[4]),
                        DocumentType = cols[5],
                        IssueDate = DateOnly.Parse(cols[6], culture),
                        DueDate = DateOnly.Parse(cols[7], culture),
                        PaymentTermsDays = int.Parse(cols[8], culture),
                        Currency = cols[9],
                        FxRateToBase = decimal.Parse(cols[10], culture),
                        NetAmount = decimal.Parse(cols[11], culture),
                        DiscountAmount = decimal.Parse(cols[12], culture),
                        VatAmount = decimal.Parse(cols[13], culture),
                        TotalAmount = decimal.Parse(cols[14], culture),
                        RetentionPct = decimal.Parse(cols[15], culture),
                        RetentionAmount = decimal.Parse(cols[16], culture),
                        Status = SeedingHelpers.ParseInvoiceStatus(cols[17]),
                        ReferenceUuid = string.IsNullOrWhiteSpace(cols[18]) ? null : cols[18],
                        CreatedAt = DateTimeOffset.UtcNow
                    });
                }
                await context.SaveChangesAsync();
            }
            // Seed Invoice Lines
            var linePath = Path.Combine(seedFolder, "invoice_line.csv");

            if (File.Exists(linePath) && !await context.InvoiceLines.AnyAsync())
            {
                var lines = await File.ReadAllLinesAsync(linePath);
                foreach (var line in lines.Skip(1))
                {
                    var cols = SeedingHelpers.ParseCsvLine(line);
                    if (cols.Length < 13) continue;
                    context.InvoiceLines.Add(new InvoiceLine
                    {
                        LineId = cols[0],
                        InvoiceUuid = cols[1],
                        LineNumber = int.Parse(cols[2], culture),
                        Description = cols[3],
                        ItemCode = cols[4],
                        UnitType = cols[5],
                        Quantity = decimal.Parse(cols[6], culture),
                        UnitPrice = decimal.Parse(cols[7], culture),
                        DiscountAmount = decimal.Parse(cols[8], culture),
                        NetAmount = decimal.Parse(cols[9], culture),
                        VatRate = decimal.Parse(cols[10], culture),
                        VatAmount = decimal.Parse(cols[11], culture),
                        TotalAmount = decimal.Parse(cols[12], culture)
                    });
                }
                await context.SaveChangesAsync();
            }
            // Seed Settlements
            var settlementPath = Path.Combine(seedFolder, "settlement.csv");

            if (File.Exists(settlementPath) && !await context.Settlements.AnyAsync())
            {
                var lines = await File.ReadAllLinesAsync(settlementPath);
                foreach (var line in lines.Skip(1))
                {
                    var cols = SeedingHelpers.ParseCsvLine(line);
                    if (cols.Length < 10) continue;
                    context.Settlements.Add(new Settlement
                    {
                        SettlementId = cols[0],
                        InvoiceUuid = cols[1],
                        CompanyId = cols[2],
                        PaidDate = DateOnly.Parse(cols[3], culture),
                        PaidAmount = decimal.Parse(cols[4], culture),
                        Currency = cols[5],
                        Method = cols[6],
                        SequenceNo = int.Parse(cols[7], culture),
                        IsFinal = bool.Parse(cols[8]),
                        BankTransactionId = string.IsNullOrWhiteSpace(cols[9]) ? null : cols[9]
                    });
                }
                await context.SaveChangesAsync();
            }
            // Seed Bank Transactions
            var txPath = Path.Combine(seedFolder, "bank_transaction.csv");

            if (File.Exists(txPath) && !await context.BankTransactions.AnyAsync())
            {
                var lines = await File.ReadAllLinesAsync(txPath);
                foreach (var line in lines.Skip(1))
                {
                    var cols = SeedingHelpers.ParseCsvLine(line);
                    if (cols.Length < 13) continue;
                    context.BankTransactions.Add(new BankTransaction
                    {
                        TransactionId = cols[0],
                        AccountId = cols[1],
                        CompanyId = cols[2],
                        BookingDate = DateOnly.Parse(cols[3], culture),
                        ValueDate = DateOnly.Parse(cols[4], culture),
                        Amount = decimal.Parse(cols[5], culture),
                        CreditDebit = cols[6],
                        Currency = cols[7],
                        BankTxCode = cols[8],
                        EndToEndId = string.IsNullOrWhiteSpace(cols[9]) ? null : cols[9],
                        RemittanceInfo = string.IsNullOrWhiteSpace(cols[10]) ? null : cols[10],
                        CounterpartyName = string.IsNullOrWhiteSpace(cols[11]) ? null : cols[11],
                        RunningBalance = decimal.Parse(cols[12], culture)
                    });
                }
                await context.SaveChangesAsync();
            }
            // Seed Recurring Obligations
            var obligationPath = Path.Combine(seedFolder, "recurring_obligation.csv");

            if (File.Exists(obligationPath) && !await context.RecurringObligations.AnyAsync())
            {
                var lines = await File.ReadAllLinesAsync(obligationPath);
                foreach (var line in lines.Skip(1))
                {
                    var cols = SeedingHelpers.ParseCsvLine(line);
                    if (cols.Length < 12) continue;
                    context.RecurringObligations.Add(new RecurringObligation
                    {
                        ObligationId = cols[0],
                        CompanyId = cols[1],
                        Kind = cols[2],
                        Description = cols[3],
                        Amount = decimal.Parse(cols[4], culture),
                        Currency = cols[5],
                        Frequency = cols[6],
                        DayOfMonth = int.Parse(cols[7], culture),
                        BusinessDayRule = cols[8],
                        VariabilityPct = decimal.Parse(cols[9], culture),
                        StartDate = DateOnly.Parse(cols[10], culture),
                        EndDate = string.IsNullOrWhiteSpace(cols[11]) ? null : DateOnly.Parse(cols[11], culture)
                    });
                }
                await context.SaveChangesAsync();
            }
            // Seed FX Rates
            var fxPath = Path.Combine(seedFolder, "fx_rate.csv");
            if (File.Exists(fxPath) && !await context.FxRates.AnyAsync())
            {
                var lines = await File.ReadAllLinesAsync(fxPath);
                foreach (var line in lines.Skip(1))
                {
                    var cols = SeedingHelpers.ParseCsvLine(line);
                    if (cols.Length < 4) continue;
                    context.FxRates.Add(new FxRate
                    {
                        RateDate = DateOnly.Parse(cols[0], culture),
                        BaseCurrency = cols[1],
                        QuoteCurrency = cols[2],
                        Rate = decimal.Parse(cols[3], culture)
                    });
                }
                await context.SaveChangesAsync();
            }
        }
    }

}
