using CashOverflow.Application.Common.Interfaces;
using CashOverflow.Application.Dtos.Onboarding;
using CashOverflow.Domain.Entities;
using CashOverflow.Domain.Enums;
using CashOverflow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CashOverflow.Infrastructure.Onboarding;

public class OnboardingService : IOnboardingService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEmailService _emailService;

    public OnboardingService(
        ApplicationDbContext dbContext,
        IEmailService emailService)
    {
        _dbContext = dbContext;
        _emailService = emailService;
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
}