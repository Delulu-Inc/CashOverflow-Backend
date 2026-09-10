namespace CashOverflow.Domain.Entities;

public class BankTransaction
{
    public string TransactionId { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public DateOnly BookingDate { get; set; }

    public DateOnly ValueDate { get; set; }

    public decimal Amount { get; set; }

    public string CreditDebit { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public string BankTxCode { get; set; } = null!;

    public string? EndToEndId { get; set; }

    public string? RemittanceInfo { get; set; }

    public string? CounterpartyName { get; set; }

    public decimal RunningBalance { get; set; }

    // Navigation Properties
    public BankAccount BankAccount { get; set; } = null!;

    public Company Company { get; set; } = null!;

    public List<Settlement> Settlements { get; set; }
}