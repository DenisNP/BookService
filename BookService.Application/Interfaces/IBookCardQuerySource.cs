using BookService.Application.ViewModels;

namespace BookService.Application.Interfaces;

public interface IBookCardQuerySource
{
    public Task<BookCardViewModel> FindByIdAsync(string bookId, bool includeUnpublished = false);
    public Task Upsert(BookCardViewModel bookCard);
}