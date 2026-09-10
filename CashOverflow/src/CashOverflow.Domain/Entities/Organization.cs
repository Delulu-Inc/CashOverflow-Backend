namespace CashOverflow.Domain.Entities;

public class Organization
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    // Navigation Properties
    public ICollection<OrganizationMember> Members { get; set; }
        = new List<OrganizationMember>();

    public ICollection<Invitation> Invitations { get; set; }
        = new List<Invitation>();

    public Subscription? Subscription { get; set; }

    public Company? Company { get; set; }
}