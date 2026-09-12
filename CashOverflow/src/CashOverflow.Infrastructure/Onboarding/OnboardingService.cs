using CashOverflow.Application.Common.Interfaces;
using CashOverflow.Application.Dtos.Onboarding;
using CashOverflow.Domain.Entities;
using CashOverflow.Domain.Enums;
using CashOverflow.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CashOverflow.Infrastructure.Onboarding;

public class OnboardingService : IOnboardingService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEmailService _emailService;
    private readonly UserManager<ApplicationUser> _userManager;

    public OnboardingService(
    ApplicationDbContext dbContext,
    IEmailService emailService,
    UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task<Guid> SubmitDemoRequestAsync(
        CreateDemoRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var emailExists = await _dbContext.DemoRequests
            .AnyAsync(
                x => x.CompanyEmail == request.CompanyEmail
                     && x.Status == DemoRequestStatus.Pending,
                cancellationToken
            );

        if (emailExists)
        {
            throw new InvalidOperationException(
                "A pending demo request already exists for this email."
            );
        }

        var demoRequest = new DemoRequest
        {
            Id = Guid.NewGuid(),

            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),

            CompanyEmail = request.CompanyEmail
                .Trim()
                .ToLowerInvariant(),

            CompanyName = request.CompanyName.Trim(),
            CompanySize = request.CompanySize.Trim(),
            PhoneNumber = request.PhoneNumber.Trim(),
            Message = request.Message.Trim(),

            Status = DemoRequestStatus.Pending,

            SubmittedAt = DateTimeOffset.UtcNow
        };

        _dbContext.DemoRequests.Add(demoRequest);

        await _dbContext.SaveChangesAsync(cancellationToken);

        await _emailService.SendDemoRequestNotificationAsync(
      demoRequest.FirstName,
      demoRequest.LastName,
      demoRequest.CompanyName,
      demoRequest.CompanySize,
      demoRequest.CompanyEmail,
      demoRequest.PhoneNumber,
      demoRequest.Message,
      cancellationToken
  );

        return demoRequest.Id;
    }

    public async Task<IReadOnlyList<DemoRequestDto>>
    GetPendingDemoRequestsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.DemoRequests
            .AsNoTracking()
            .Where(x => x.Status == DemoRequestStatus.Pending)
            .OrderByDescending(x => x.SubmittedAt)
            .Select(x => new DemoRequestDto
            {
                Id = x.Id,

                FirstName = x.FirstName,
                LastName = x.LastName,

                CompanyEmail = x.CompanyEmail,
                CompanyName = x.CompanyName,

                CompanySize = x.CompanySize,
                PhoneNumber = x.PhoneNumber,

                Message = x.Message,

                Status = x.Status.ToString(),

                SubmittedAt = x.SubmittedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task ApproveDemoRequestAsync(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        var demoRequest = await _dbContext.DemoRequests
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (demoRequest == null)
        {
            throw new KeyNotFoundException(
                "Demo request not found.");
        }

        if (demoRequest.Status != DemoRequestStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending demo requests can be approved.");
        }

        var token = Convert.ToHexString(
            RandomNumberGenerator.GetBytes(32));

        var invitation = new Invitation
        {
            Id = Guid.NewGuid(),
            OrganizationId = "DEMO",
            Role = "Owner",
            Email = demoRequest.CompanyEmail,
            Token = token,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(7),
            AcceptedAt = null
        };

        _dbContext.Invitations.Add(invitation);

        demoRequest.Status = DemoRequestStatus.Contacted;

        await _dbContext.SaveChangesAsync(cancellationToken);

        var invitationLink =
            $"http://localhost:5173/accept-invite?token={token}";

        await _emailService.SendInvitationAsync(
            demoRequest.CompanyEmail,
            invitationLink,
            cancellationToken);
    }
    public async Task AcceptInviteAsync(
    AcceptInviteDto request,
    CancellationToken cancellationToken = default)
    {
        if (request.Password != request.ConfirmPassword)
        {
            throw new InvalidOperationException(
                "Password and confirmation password do not match.");
        }

        var invitation = await _dbContext.Invitations
            .FirstOrDefaultAsync(
                x => x.Token == request.Token,
                cancellationToken);

        if (invitation == null)
        {
            throw new KeyNotFoundException(
                "Invitation not found.");
        }

        if (invitation.AcceptedAt != null)
        {
            throw new InvalidOperationException(
                "Invitation has already been accepted.");
        }

        if (invitation.ExpiresAt <= DateTimeOffset.UtcNow)
        {
            throw new InvalidOperationException(
                "Invitation has expired.");
        }

        var existingUser =
            await _userManager.FindByEmailAsync(invitation.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        var demoRequest = await _dbContext.DemoRequests
            .FirstOrDefaultAsync(
                x => x.CompanyEmail == invitation.Email
                     && x.Status == DemoRequestStatus.Contacted,
                cancellationToken);

        if (demoRequest == null)
        {
            throw new InvalidOperationException(
                "Related demo request was not found.");
        }

        var user = new ApplicationUser
        {
            UserName = invitation.Email,
            Email = invitation.Email,
            FirstName = demoRequest.FirstName,
            LastName = demoRequest.LastName,
            EmailConfirmed = true
        };

        var createUserResult =
            await _userManager.CreateAsync(
                user,
                request.Password);

        if (!createUserResult.Succeeded)
        {
            var errors = string.Join(
                ", ",
                createUserResult.Errors.Select(x => x.Description));

            throw new InvalidOperationException(errors);
        }

        var roleResult =
            await _userManager.AddToRoleAsync(
                user,
                invitation.Role);

        if (!roleResult.Succeeded)
        {
            var errors = string.Join(
                ", ",
                roleResult.Errors.Select(x => x.Description));

            throw new InvalidOperationException(errors);
        }

        var organizationMember = new OrganizationMember
        {
            Id = Guid.NewGuid(),
            Name = $"{user.FirstName} {user.LastName}",
            ApplicationUserId = user.Id,
            OrganizationId = invitation.OrganizationId
        };

        _dbContext.OrganizationMembers.Add(
            organizationMember);

        invitation.AcceptedAt =
            DateTimeOffset.UtcNow;

        demoRequest.Status =
            DemoRequestStatus.Closed;

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}