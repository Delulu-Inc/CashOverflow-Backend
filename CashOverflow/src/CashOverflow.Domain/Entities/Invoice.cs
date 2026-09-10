using CashOverflow.Domain.Enums;

namespace CashOverflow.Domain.Entities;

public class Invoice
{
    public string Uuid { get; set; } = null!;

    public string InternalId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public string CounterpartyId { get; set; } = null!;

    public InvoiceDirection Direction { get; set; }

    public InvoiceStatus Status { get; set; }
    public string DocumentType { get; set; } = null!;

    public DateOnly IssueDate { get; set; }

    public DateOnly DueDate { get; set; }

    public int PaymentTermsDays { get; set; }

    public string Currency { get; set; } = null!;

    public decimal FxRateToBase { get; set; }

    public decimal NetAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal VatAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal RetentionPct { get; set; }

    public decimal RetentionAmount { get; set; }


    public string? ReferenceUuid { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    // Navigation Properties
    public Company Company { get; set; } = null!;

    public CounterParty Counterparty { get; set; } = null!;

    public ICollection<InvoiceLine> InvoiceLines { get; set; }
        = new List<InvoiceLine>();

    public ICollection<Settlement> Settlements { get; set; }
        = new List<Settlement>();
}