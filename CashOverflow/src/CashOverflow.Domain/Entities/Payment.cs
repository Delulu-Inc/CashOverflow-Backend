using CashOverflow.Domain.Enums;

namespace CashOverflow.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }

<<<<<<< HEAD
    public string OrganizationId { get; set; } = null!;

    public Guid? SubscriptionId { get; set; }

=======
    public Guid SubscriptionId { get; set; }
    public string OrganizationId { get; set; } = null!;
>>>>>>> 800e601362b76cd1a82c7f1502c229f9c012c179
    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public PaymentMethod Method { get; set; }

    public PaymentStatus Status { get; set; }

    public string? ProviderPaymentId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? PaidAt { get; set; }

<<<<<<< HEAD
    // Navigation Properties
    public Organization Organization { get; set; } = null!;

    public Subscription? Subscription { get; set; }
=======
    // Navigation Property
    public Subscription Subscription { get; set; } = null!;
    public Organization Organization { get; set; } = null!;
>>>>>>> 800e601362b76cd1a82c7f1502c229f9c012c179
}