using System.Text.Json;

namespace App.Infrastructure.Core.DataBaseContext.Audit;

public sealed class AuditMessage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public DateTime OccurredAtUtc { get; private set; }
    public string? Error { get; set; }

    private AuditMessage() { }

    public AuditMessage(object domainEvent)
    {
        Id = Guid.NewGuid();
        Type = domainEvent.GetType().AssemblyQualifiedName!;
        Content = JsonSerializer.Serialize(domainEvent, domainEvent.GetType());
        OccurredAtUtc = DateTime.UtcNow;
    }
}
