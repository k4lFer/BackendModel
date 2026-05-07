using App.Objects.User.DTOs.Input.Command;
using App.Shared.Result;
using Cortex.Mediator.Commands;

namespace App.UseCases.User.Command.CreateUser;

public class CreateUserCommand : ICommand<OutputPort<Guid>>
{
    public CreateUserDto Input { get; }
    
    public CreateUserCommand(CreateUserDto input)
    {
        Input = input;
    }
}