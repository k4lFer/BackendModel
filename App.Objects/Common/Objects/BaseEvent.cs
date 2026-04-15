using Cortex.Mediator.Notifications;

namespace App.Objects.Common.Objects;

public class BaseEvent : INotification
{
    public DateTime DateOcurred { get; protected  set; } = DateTime.UtcNow;
}