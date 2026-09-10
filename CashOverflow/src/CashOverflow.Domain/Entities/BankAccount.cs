namespace CashOverflow.Domain.Entities;

public class BankAccount
{
    public string AccountId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public string Iban { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public decimal OpeningBalance { get; set; }

    public DateOnly OpenedDate { get; set; }

    // Navigation Properties
    public Company Company { get; set; } = null!;

    public ICollection<BankTransaction> BankTransactions { get; set; }
        = new List<BankTransaction>();
}