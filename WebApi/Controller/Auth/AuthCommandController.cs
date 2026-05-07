using App.Objects.User.DTOs.Input.Command;
using Cortex.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helper;

namespace WebApi.Controller.Auth;

[ApiController]
[Route("api/auth")]
[Tags("Auth")]
public class AuthCommandController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthCommandController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("[action]")]
    [AllowAnonymous]
    [EndpointSummary("Registrar usuario")]
    [EndpointDescription("Permite a un usuario registrarse en el sistema")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto dto, CancellationToken cancellationToken)
    {        
        var command = new App.UseCases.Auth.Command.SignUp.SignUpCommand(dto);
        var result = await _mediator.SendCommandAsync(command, cancellationToken);
        return ResponseHelper.GetActionResult(result);
    }

    [HttpPost]
    [Route("[action]")]
    [AllowAnonymous]
    [EndpointSummary("Iniciar sesión")]
    [EndpointDescription("Permite a un usuario iniciar sesión en el sistema")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
    {
        var command = new App.UseCases.Auth.Command.Login.LoginCommand(dto);
        var result = await _mediator.SendCommandAsync(command, cancellationToken);
        return ResponseHelper.GetActionResult(result);
    }
}
