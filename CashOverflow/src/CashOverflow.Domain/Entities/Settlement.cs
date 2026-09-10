namespace CashOverflow.Domain.Entities;

public class Settlement
{
    public string SettlementId { get; set; } = null!;

    public string InvoiceUuid { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public DateOnly PaidDate { get; set; }

    public decimal PaidAmount { get; set; }

    public string Currency { get; set; } = null!;

    public string Method { get; set; } = null!;

    public int SequenceNo { get; set; }

    public bool IsFinal { get; set; }

    public string? BankTransactionId { get; set; }

    // Navigation Properties
    public Invoice Invoice { get; set; } = null!;

    public Company Company { get; set; } = null!;

    public BankTransaction? BankTransaction { get; set; }
}