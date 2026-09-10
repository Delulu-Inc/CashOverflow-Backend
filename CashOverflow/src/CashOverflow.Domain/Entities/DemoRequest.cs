using CashOverflow.Domain.Enums;

namespace CashOverflow.Domain.Entities;

public class DemoRequest
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string CompanyEmail { get; set; } = null!;

    public string CompanyName { get; set; } = null!;
    public DemoRequestStatus Status { get; set; }
    public string Message { get; set; } = null!;


    public DateTimeOffset SubmittedAt { get; set; }
}