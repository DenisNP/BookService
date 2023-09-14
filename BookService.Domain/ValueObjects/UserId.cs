using BookService.Domain.Common;

namespace BookService.Domain.ValueObjects;

public sealed record UserId : EntityId
{
    public UserId(string value) : base(value)
    {
    }

    public static UserId CreateNew()
    {
        return new UserId(Guid.NewGuid().ToString());
    }
}