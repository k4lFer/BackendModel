using App.Objects.User.DTOs.Input.Command;
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
        
}