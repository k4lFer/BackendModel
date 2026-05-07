using App.Objects.User.DTOs.Input.Command;
using App.Objects.User.DTOs.Output.Response;
using App.Shared.Result;
using Cortex.Mediator.Commands;

namespace App.UseCases.Auth.Command.Login;

public class LoginCommand : ICommand<OutputPort<LoginResponseDto>>
{
    public LoginDto Input { get; }
    
    public LoginCommand(LoginDto input)
    {
        Input = input;
    }
}
