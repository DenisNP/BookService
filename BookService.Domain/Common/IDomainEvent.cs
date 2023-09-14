namespace BookService.Domain.Common;

public interface IDomainEvent
{
    public void Accept(IDomainEventVisitor visitor);
}