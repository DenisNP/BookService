using BookService.Domain.Common;
using BookService.Domain.ValueObjects;

namespace BookService.Domain.Entities;

public class Link : Entity<LinkId>
{
    public string FromId { get; }
    public string ToId { get; }
    
    public Link(LinkId id, string fromId, string toId) : base(id)
    {
        if (string.IsNullOrEmpty(fromId))
        {
            throw new Exception($"{nameof(fromId)} cannt be empty");
        }

        if (string.IsNullOrEmpty(toId))
        {
            throw new Exception($"{nameof(toId)} cannt be empty");
        }

        FromId = fromId;
        ToId = toId;
    }
}