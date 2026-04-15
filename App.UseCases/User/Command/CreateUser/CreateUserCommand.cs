using App.Interfaces.Common.Result;
using App.Objects.User.DTOs.Input.Command;
using Cortex.Mediator.Commands;

namespace App.UseCases.User.Command.CreateUser;

public class CreateUserCommand : ICommand<OutputPort<Guid>>
{
    public CreateUserDto Input { get; set; }
    
    public CreateUserCommand(CreateUserDto input)
    {
        Input = input;
    }
}