namespace CashOverflow.Domain.Entities;

public class RecurringObligation
{
    public string ObligationId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string Frequency { get; set; } = null!;

    public int DayOfMonth { get; set; }

    public string BusinessDayRule { get; set; } = null!;

    public decimal VariabilityPct { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    // Navigation Property
    public Company Company { get; set; } = null!;
}