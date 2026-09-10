namespace CashOverflow.Domain.Entities;

public class FxRate
{
    public DateOnly RateDate { get; set; }

    public string BaseCurrency { get; set; } = null!;

    public string QuoteCurrency { get; set; } = null!;

    public decimal Rate { get; set; }
}