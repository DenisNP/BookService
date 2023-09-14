using BookService.Application.Interfaces;
using BookService.Application.ViewModels;
using BookService.Domain.Common;
using BookService.Domain.DomainEvents;
using BookService.Domain.Entities;
using BookService.Domain.ValueObjects;

namespace BookService.Infrastructure;

public class BookStateChangedVisitor : IDomainEventVisitor<BookStateChangedDomainEvent>
{
    private readonly IBookCardQuerySource _bookCardQuerySource;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILinkRepository _linkRepository;

    public BookStateChangedVisitor(
        IBookCardQuerySource bookCardQuerySource,
        IBookRepository bookRepository,
        IUserRepository userRepository,
        ILinkRepository linkRepository
    )
    {
        _bookCardQuerySource = bookCardQuerySource;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _linkRepository = linkRepository;
    }
    
    public async Task Visit(BookStateChangedDomainEvent @event)
    {
        // в реальной жизни нужно дополнительные проверки на существование всех объектов,
        // но в примере представим, что они точно есть
        Book book = await _bookRepository.FindByIdAsync(@event.BookId);
        IReadOnlyList<Link> links = await _linkRepository.FindAllLinksFrom(@event.BookId.ToString());

        // если никакой пользователь не был связан с книгой,
        // отобразить карточку такой книги всё равно можно,
        // просто часть "текущий читатель" будет не заполнена
        Link linkToUser = links.FirstOrDefault();
        User user = null;
        if (linkToUser != null)
        {
            var userId = new UserId(linkToUser.ToId);
            user = await _userRepository.FindByIdAsync(userId);
        }

        // ищем, существует ли карточка книги
        // если нет, создаём новую
        BookCardViewModel bookCard = await _bookCardQuerySource.FindByIdAsync(book.Id.ToString(), true)
                                     ?? new BookCardViewModel
                                     {
                                         Id = book.Id.ToString(),
                                         AuthorName = book.AuthorName,
                                         Title = book.Title
                                     };

        // устанавливаем в карточку новые значения
        bookCard.PageCount = book.Pages.Count;
        bookCard.IsPublished = book.Published;
        bookCard.State = new BookStateViewModel
        {
            IsOpened = book.Opened,
            CurrentPage = book.CurrentPage,
            CurrentReader = user == null 
                ? null 
                : new UserViewModel{ Id = user.Id.ToString(), Name = user.Name, Age = user.Age }
        };

        // сохраняем изменения или добавляем новый объект
        await _bookCardQuerySource.Upsert(bookCard);
    }
}