using App.Shared.Security;
using App.UseCases.User.Query.MyProfile;
using Cortex.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helper;

namespace WebApi.Controller.User;

[ApiController]
[Route("api/user")]
[Tags("Users")]
public class UserQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUser _currentUser;

    public UserQueryController(IMediator mediator, ICurrentUser currentUser)
    {
        _mediator = mediator;
        _currentUser = currentUser;
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize]
    public async Task<IActionResult> GetUserInfo(CancellationToken cancellationToken)
    {
        return null;
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize]
    public async Task<IActionResult> MyProfile(CancellationToken cancellationToken)
    {
        var userClaims = _currentUser.GetClaim();
        if (userClaims is null) return Unauthorized();
        var query = new MyProfileQuery(userClaims.Id);
        var result = await _mediator.SendQueryAsync(query, cancellationToken);
        return ResponseHelper.GetActionResult(result);
    }
}