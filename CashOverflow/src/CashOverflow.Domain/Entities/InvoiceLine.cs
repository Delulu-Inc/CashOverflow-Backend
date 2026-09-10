namespace CashOverflow.Domain.Entities;

public class InvoiceLine
{
    public string LineId { get; set; } = null!;

    public string InvoiceUuid { get; set; } = null!;

    public int LineNumber { get; set; }

    public string Description { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public string UnitType { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal NetAmount { get; set; }

    public decimal VatRate { get; set; }

    public decimal VatAmount { get; set; }

    public decimal TotalAmount { get; set; }

    // Navigation Property
    public Invoice Invoice { get; set; } = null!;
}