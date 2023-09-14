using BookService.Domain.Common;

namespace BookService.Domain.ValueObjects;

public sealed record LinkId : EntityId
{
    public LinkId(string value) : base(value)
    {
    }

    public static LinkId CreateNew()
    {
        return new LinkId(Guid.NewGuid().ToString());
    }
}