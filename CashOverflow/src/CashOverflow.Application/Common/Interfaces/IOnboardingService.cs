using CashOverflow.Application.Dtos.Onboarding;

namespace CashOverflow.Application.Common.Interfaces;

public interface IOnboardingService
{
    Task<Guid> SubmitDemoRequestAsync(
        CreateDemoRequestDto request,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DemoRequestDto>> GetPendingDemoRequestsAsync(
    CancellationToken cancellationToken = default);
    Task ApproveDemoRequestAsync(
    Guid id,
    CancellationToken cancellationToken = default);

    Task AcceptInviteAsync(
    AcceptInviteDto request,
    CancellationToken cancellationToken = default);
}