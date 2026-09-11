using CashOverflow.Application.Dtos.Onboarding;

namespace CashOverflow.Application.Common.Interfaces;

public interface IOnboardingService
{
    Task<Guid> SubmitDemoRequestAsync(
        CreateDemoRequestDto request,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DemoRequestDto>> GetPendingDemoRequestsAsync(
    CancellationToken cancellationToken = default);
}