namespace CashOverflow.Domain.Entities;

public class ApplicationUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? AvatarUrl { get; set; }

    // Navigation Properties
    public List<OrganizationMember> OrganizationMembers { get; set; } = new List<OrganizationMember>();
}