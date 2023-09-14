namespace BookService.Domain.Common;

public abstract record EntityId
{
    protected readonly string Value;
    protected EntityId(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception("ID cannot be null");
        }
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}