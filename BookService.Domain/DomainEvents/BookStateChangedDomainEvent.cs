using BookService.Domain.Common;
using BookService.Domain.ValueObjects;

namespace BookService.Domain.DomainEvents;

public record BookStateChangedDomainEvent(BookId BookId) : IDomainEvent
{
    public void Accept(IDomainEventVisitor visitor)
    {
        // если в visitor нет реализации для нужного типа события, то выражение
        // в скобках будет null, поэтому можно безопасно попробовать запустить метод Visit
        (visitor as IDomainEventVisitor<BookStateChangedDomainEvent>)?.Visit(this);
    }
}