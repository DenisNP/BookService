namespace BookService.Domain.Common;

public interface IDomainEventVisitor
{
}

public interface IDomainEventVisitor<in TDomainEvent> : IDomainEventVisitor where TDomainEvent : IDomainEvent
{
    public Task Visit(TDomainEvent @event);
}