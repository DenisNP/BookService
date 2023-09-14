namespace BookService.Domain.Common;

public abstract class Entity<TId> where TId : EntityId
{
    public TId Id { get; }
    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();
    private readonly List<IDomainEvent> _events = new();

    protected Entity(TId id)
    {
        Id = id ?? throw new Exception("Id cannot be null");
    }

    protected void RegisterEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }
}