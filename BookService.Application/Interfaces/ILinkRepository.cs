using BookService.Domain.Entities;

namespace BookService.Application.Interfaces;

public interface ILinkRepository
{
    public Task<IReadOnlyList<Link>> FindAllLinksFrom(string fromId);
    public Task Add(Link link);
    public Task Delete(Link link);
}