namespace CashOverflow.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendDemoRequestNotificationAsync(
      string firstName,
      string lastName,
      string companyName,
      string companySize,
      string companyEmail,
      string phoneNumber,
      string message,
      CancellationToken cancellationToken = default);

    Task SendInvitationAsync(
        string email,
        string invitationLink,
        CancellationToken cancellationToken = default);
}