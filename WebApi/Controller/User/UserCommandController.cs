using App.Objects.User.DTOs.Input.Command;
using App.UseCases.User.Command.CreateUser;
using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helper;

namespace WebApi.Controller.User;

[ApiController]
[Route("api/user")]
public class UserCommandController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserCommandController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(createUserDto);
        var result = await _mediator.SendCommandAsync(command, cancellationToken);
        return ResponseHelper.GetActionResult(result);
    }
        
}