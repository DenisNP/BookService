using BookService.Domain.Common;

namespace BookService.Domain.ValueObjects;

public sealed record BookId : EntityId
{
    public BookId(string value) : base(value)
    {
    }

    public static BookId CreateNew()
    {
        return new BookId(Guid.NewGuid().ToString());
    }
}