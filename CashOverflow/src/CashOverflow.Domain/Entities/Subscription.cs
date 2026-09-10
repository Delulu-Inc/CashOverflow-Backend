using CashOverflow.Domain.Enums;
namespace CashOverflow.Domain.Entities;

public class Subscription
{
    public Guid Id { get; set; }

    public string OrganizationId { get; set; } = null!;

    public string SubscriptionPlan { get; set; } = null!;


    public SubscriptionStatus Status { get; set; }
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    // Navigation Properties
    public Organization Organization { get; set; } = null!;

    public ICollection<Payment> Payments { get; set; }
        = new List<Payment>();
}