using BookService.Application.Interfaces;
using BookService.Domain.Entities;

namespace BookService.Infrastructure.Repositories;

public class LinkRepository : ILinkRepository
{
    public Task<IReadOnlyList<Link>> FindAllLinksFrom(string fromId)
    {
        throw new NotImplementedException();
    }

    public Task Add(Link link)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Link link)
    {
        throw new NotImplementedException();
    }
}