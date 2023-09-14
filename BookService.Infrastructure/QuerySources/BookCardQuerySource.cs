using BookService.Application.Interfaces;
using BookService.Application.ViewModels;

namespace BookService.Infrastructure.QuerySources;

public class BookCardQuerySource : IBookCardQuerySource
{
    public Task<BookCardViewModel> FindByIdAsync(string bookId, bool includeUnpublished = false)
    {
        throw new NotImplementedException();
    }

    public Task Upsert(BookCardViewModel bookCard)
    {
        throw new NotImplementedException();
    }
}