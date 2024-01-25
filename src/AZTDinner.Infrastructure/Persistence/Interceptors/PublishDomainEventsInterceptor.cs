using AZTDinner.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AZTDinner.Infrastructure.Persistence.Inteceptors;
public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _mediator;

    public PublishDomainEventsInterceptor(IPublisher mediator)
    {
        _mediator = mediator;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavedChanges(eventData, result);
    }
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private async Task PublishDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return;
        }
        //Get hold of all the various entities
        var entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
                                                              .Where(entry => entry.Entity.DomainEvents.Any())
                                                              .Select(entry => entry.Entity)
                                                              .ToList();
        //Get hold of all the various domain events
        var domainEvents = entitiesWithDomainEvents.SelectMany(entry => entry.DomainEvents).ToList();

        entitiesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

    }
}