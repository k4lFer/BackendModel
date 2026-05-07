using App.Shared.Domain;
using Cortex.Mediator;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Infrastructure.Core.DataBaseContext.Interceptors;

public sealed class DomainEventDispatcherInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public DomainEventDispatcherInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData, 
        int result, 
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return await base.SavedChangesAsync(eventData, result, cancellationToken);

        // 1. Extraemos las entidades que tienen eventos
        var domainEntities = eventData.Context.ChangeTracker
            .Entries<BaseDomain>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        // 2. Extraemos los eventos
        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        // 3. Limpiamos los eventos de la memoria local
        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        // 4. Emitimos los eventos a todos los manejadores (ej. el que enviará el email)
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.PublishAsync(domainEvent, cancellationToken);
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
