using App.Shared.Result;
using Cortex.Mediator.Commands;

namespace App.UseCases.Auth.Command.RevokeAllSessions;

public class RevokeAllSessionsCommand : ICommand<OutputPort<object>>
{
    public Guid UserId { get; }

    public RevokeAllSessionsCommand(Guid userId)
    {
        UserId = userId;
    }
}