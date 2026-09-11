namespace CashOverflow.Application.Dtos.Onboarding;

public class DemoRequestDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string CompanyEmail { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string CompanySize { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTimeOffset SubmittedAt { get; set; }
}