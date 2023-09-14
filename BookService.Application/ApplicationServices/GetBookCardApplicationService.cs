using BookService.Application.Interfaces;
using BookService.Application.RequestModels;
using BookService.Application.ViewModels;

namespace BookService.Application.ApplicationServices;

public class GetBookCardApplicationService : IApplicationService<GetBookCardRequest, BookCardViewModel>
{
    private readonly IBookCardQuerySource _bookCardQuerySource;

    public GetBookCardApplicationService(IBookCardQuerySource bookCardQuerySource)
    {
        _bookCardQuerySource = bookCardQuerySource;
    }

    public async Task<BookCardViewModel> Handle(GetBookCardRequest request)
    {
        // фильтр в данном примере представлен единственным параметром IncludeUnpublished,
        // но вообще он может быть любой сложности
        BookCardViewModel book = await _bookCardQuerySource.FindByIdAsync(request.BookId, request.IncludeUnpublished);
        // в зависимости от бизнес-сценария в этом месте можно бросить исключение,
        // если книга не найдена, либо так и вернуть null
        return book;
    }
}