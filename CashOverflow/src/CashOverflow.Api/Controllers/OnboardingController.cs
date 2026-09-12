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
    //[Authorize]
    [HttpGet("demo-requests")]
    public async Task<IActionResult> GetPendingDemoRequests(
    CancellationToken cancellationToken)
    {
        var requests = await _onboardingService
            .GetPendingDemoRequestsAsync(cancellationToken);

        return Ok(requests);
    }

  //  [Authorize]
    [HttpPost("approve/{id:guid}")]
    public async Task<IActionResult> ApproveDemoRequest(
    Guid id,
    CancellationToken cancellationToken)
    {
        try
        {
            await _onboardingService.ApproveDemoRequestAsync(
                id,
                cancellationToken);

            return Ok(new
            {
                message = "Demo request approved and invitation sent successfully."
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }


    [AllowAnonymous]
    [HttpPost("accept-invite")]
    public async Task<IActionResult> AcceptInvite(
    [FromBody] AcceptInviteDto request,
    CancellationToken cancellationToken)
    {
        try
        {
            await _onboardingService.AcceptInviteAsync(
                request,
                cancellationToken);

            return Ok(new
            {
                message = "Invitation accepted successfully."
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }
}