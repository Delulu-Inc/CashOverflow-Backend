using CashOverflow.Domain.Enums;

namespace CashOverflow.Domain.Entities;

public class CounterParty
{
    public string CounterpartyId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? TaxId { get; set; }

    public CounterPartyRole Role { get; set; }
    public string Sector { get; set; } = null!;

    public string SizeBand { get; set; } = null!;

    public string Governorate { get; set; } = null!;

    public bool IsGovernment { get; set; }

    public int DefaultPaymentTermsDays { get; set; }

    public string RiskSegment { get; set; } = null!;

    public DateOnly RelationshipStart { get; set; }

    // Navigation Properties
    public Company Company { get; set; } = null!;

    public ICollection<Invoice> Invoices { get; set; }
        = new List<Invoice>();
}