namespace CashOverflow.Application.Dtos.Onboarding;

public class AcceptInviteDto
{
    public string Token { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}