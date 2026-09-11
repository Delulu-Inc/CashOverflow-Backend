using System.ComponentModel.DataAnnotations;

namespace CashOverflow.Application.Dtos.Onboarding;

public class CreateDemoRequestDto
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string CompanyEmail { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    public string CompanyName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string CompanySize { get; set; } = null!;

    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [MaxLength(1000)]
    public string Message { get; set; } = null!;
}