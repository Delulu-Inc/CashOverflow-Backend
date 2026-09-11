using CashOverflow.Domain.Enums;

namespace CashOverflow.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }

    public string OrganizationId { get; set; } = null!;

    public Guid? SubscriptionId { get; set; }


    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public PaymentMethod Method { get; set; }

    public PaymentStatus Status { get; set; }

    public string? ProviderPaymentId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? PaidAt { get; set; }


    public Organization Organization { get; set; } = null!;

    public Subscription? Subscription { get; set; }


}