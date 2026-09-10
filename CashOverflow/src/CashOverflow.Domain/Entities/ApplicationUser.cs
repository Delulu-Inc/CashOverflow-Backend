namespace CashOverflow.Domain.Entities;

public class ApplicationUser
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    // Navigation Properties
    public ICollection<OrganizationMember> OrganizationMembers { get; set; }
        = new List<OrganizationMember>();
}