using App.Objects.User.DTOs.Input.Command;
using App.Objects.User.DTOs.Output.Response;
using App.Shared.Result;
using Cortex.Mediator.Commands;

namespace App.UseCases.Auth.Command.RefreshToken;

public class RefreshTokenCommand : ICommand<OutputPort<LoginResponseDto>>
{
    public RefreshTokenDto Dto { get; }

    public RefreshTokenCommand(RefreshTokenDto dto)
    {
        Dto = dto;
    }
}