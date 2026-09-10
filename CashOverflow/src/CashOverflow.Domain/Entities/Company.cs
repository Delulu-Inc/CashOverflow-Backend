namespace CashOverflow.Domain.Entities;

public class Company
{
    public string CompanyId { get; set; } = null!;

    public string LegalName { get; set; } = null!;

    public string TaxId { get; set; } = null!;

    public string Sector { get; set; } = null!;

    public string SizeBand { get; set; } = null!;

    public string Governorate { get; set; } = null!;

    public int EmployeeCount { get; set; }

    public DateOnly FoundedDate { get; set; }

    public string BaseCurrency { get; set; } = null!;

    public decimal OpeningBalance { get; set; }

    public decimal MinCashBuffer { get; set; }

    public decimal CreditLineLimit { get; set; }

    public decimal CreditLineDrawn { get; set; }

    public bool IsDemo { get; set; }

    // Navigation Properties
    public Organization Organization { get; set; } = null!;

    public ICollection<CounterParty> Counterparties { get; set; }
        = new List<CounterParty>();

    public ICollection<BankAccount> BankAccounts { get; set; }
        = new List<BankAccount>();

    public ICollection<Invoice> Invoices { get; set; }
        = new List<Invoice>();

    public ICollection<RecurringObligation> RecurringObligations { get; set; }
        = new List<RecurringObligation>();

    public ICollection<Settlement> Settlements { get; set; }
        = new List<Settlement>();

    public ICollection<BankTransaction> BankTransactions { get; set; }
        = new List<BankTransaction>();
}