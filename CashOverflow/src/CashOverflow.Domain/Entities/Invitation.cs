namespace CashOverflow.Domain.Entities;

public class Invitation
{
    public Guid Id { get; set; }

    public string OrganizationId { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTimeOffset ExpiresAt { get; set; }

    public DateTimeOffset? AcceptedAt { get; set; }

    // Navigation Property
    public Organization Organization { get; set; } = null!;
}