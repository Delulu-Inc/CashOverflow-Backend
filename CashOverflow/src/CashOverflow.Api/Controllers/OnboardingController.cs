using CashOverflow.Application.Common.Interfaces;
using CashOverflow.Application.Dtos.Onboarding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashOverflow.Api.Controllers.DevB;

[ApiController]
[Route("api/v1/onboarding")]
public class OnboardingController : ControllerBase
{
    private readonly IOnboardingService _onboardingService;

    public OnboardingController(
        IOnboardingService onboardingService)
    {
        _onboardingService = onboardingService;
    }

    [AllowAnonymous]
    [HttpPost("demo-request")]
    public async Task<IActionResult> SubmitDemoRequest(
        [FromBody] CreateDemoRequestDto request,
        CancellationToken cancellationToken)
    {
        var id = await _onboardingService.SubmitDemoRequestAsync(
            request,
            cancellationToken
        );

        return StatusCode(
            StatusCodes.Status201Created,
            new
            {
                id,
                message = "Demo request submitted successfully."
            }
        );
    }
    //  [Authorize(Roles = "Admin,Owner")]
    [Authorize]
    [HttpGet("demo-requests")]
    public async Task<IActionResult> GetPendingDemoRequests(
    CancellationToken cancellationToken)
    {
        var requests = await _onboardingService
            .GetPendingDemoRequestsAsync(cancellationToken);

        return Ok(requests);
    }
}