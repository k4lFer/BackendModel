using App.Domain.User.Events;
using Cortex.Mediator;
using Cortex.Mediator.Notifications;
using Microsoft.Extensions.Logging;

namespace App.UseCases.User.EventHandlers.Domain;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(
        IMediator mediator,
        ILogger<UserCreatedEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain event received for user {UserId}, publishing application event", notification.UserId);

        var appEvent = new UserCreatedAppEvent(
            notification.UserId,
            notification.Email,
            notification.Username,
            notification.IsEmailConfirmed
        );

        await _mediator.PublishAsync(appEvent, cancellationToken);
    }
}
