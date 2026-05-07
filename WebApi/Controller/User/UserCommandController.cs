using App.Objects.User.DTOs.Input.Command;
using App.UseCases.User.Command.CreateUser;
using Cortex.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using WebApi.Helper;

namespace WebApi.Controller.User;

[ApiController]
[Route("api/user")]
[Tags("Users")]
public class UserCommandController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserCommandController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("[action]")]
    [AllowAnonymous]
    [EndpointSummary("Crear nuevo usuario")]
    [EndpointDescription("Crea una nueva cuenta de usuario en el sistema")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(createUserDto);
        var result = await _mediator.SendCommandAsync(command, cancellationToken);
        return ResponseHelper.GetActionResult(result);
    }
        
}