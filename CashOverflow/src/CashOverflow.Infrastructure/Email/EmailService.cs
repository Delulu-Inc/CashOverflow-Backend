using CashOverflow.Application.Common.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CashOverflow.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(
        IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendDemoRequestNotificationAsync(
     string firstName,
     string lastName,
     string companyName,
     string companySize,
     string companyEmail,
     string phoneNumber,
     string message,
     CancellationToken cancellationToken = default)
    {
        var subject = "New CashOverflow Demo Request";

        var body = $"""
        <h2>New Demo Request</h2>

        <p>
            A new company has requested a CashOverflow demo.
        </p>

        <hr />

        <p>
            <strong>Name:</strong>
            {firstName} {lastName}
        </p>

        <p>
            <strong>Company:</strong>
            {companyName}
        </p>

        <p>
            <strong>Company Size:</strong>
            {companySize}
        </p>

        <p>
            <strong>Email:</strong>
            {companyEmail}
        </p>

        <p>
            <strong>Phone:</strong>
            {phoneNumber}
        </p>

        <p>
            <strong>Message:</strong>
            {message}
        </p>

        <hr />

        <p>
            Please review the request from the CashOverflow admin dashboard.
        </p>
        """;

        await SendEmailAsync(
            _settings.FromEmail,
            subject,
            body,
            cancellationToken
        );
    
    }

    public async Task SendInvitationAsync(
        string email,
        string invitationLink,
        CancellationToken cancellationToken = default)
    {
        var subject = "You're invited to CashOverflow";

        var body = $"""
            <h2>Welcome to CashOverflow</h2>

            <p>
                Your CashOverflow demo request has been approved.
            </p>

            <p>
                Click the button below to continue:
            </p>

            <p>
                <a href="{invitationLink}"
                   style="
                       padding: 12px 20px;
                       background-color: #000000;
                       color: #ffffff;
                       text-decoration: none;
                       border-radius: 5px;">
                    Accept Invitation
                </a>
            </p>

            <p>
                If the button does not work, use this link:
            </p>

            <p>
                {invitationLink}
            </p>
            """;

        await SendEmailAsync(
            email,
            subject,
            body,
            cancellationToken
        );
    }

    private async Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken)
    {
        var message = new MimeMessage();

        message.From.Add(
            new MailboxAddress(
                _settings.FromName,
                _settings.FromEmail
            )
        );

        message.To.Add(
            MailboxAddress.Parse(toEmail)
        );

        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = htmlBody
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(
            _settings.Host,
            _settings.Port,
            SecureSocketOptions.StartTls,
            cancellationToken
        );

        await smtpClient.AuthenticateAsync(
            _settings.UserName,
            _settings.Password,
            cancellationToken
        );

        await smtpClient.SendAsync(
            message,
            cancellationToken
        );

        await smtpClient.DisconnectAsync(
            true,
            cancellationToken
        );
    }
}