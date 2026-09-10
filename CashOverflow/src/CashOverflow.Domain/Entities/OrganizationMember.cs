namespace CashOverflow.Domain.Entities;

public class OrganizationMember
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid ApplicationUserId { get; set; }

    public string OrganizationId { get; set; } = null!;

    // Navigation Properties
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public Organization Organization { get; set; } = null!;
}