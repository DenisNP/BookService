using BookService.Application.Interfaces;
using BookService.Application.RequestModels;
using BookService.Application.ViewModels;
using BookService.Domain.Entities;
using BookService.Domain.ValueObjects;

namespace BookService.Application.ApplicationServices;

public class StartReadingBookApplicationService : IApplicationService<StartReadingBookRequest, EmptyResult>
{
    private readonly IBookRepository _bookRepository;
    private readonly ILinkRepository _linkRepository;

    public StartReadingBookApplicationService(
        IBookRepository bookRepository, IUserRepository userRepository, ILinkRepository linkRepository)
    {
        _bookRepository = bookRepository;
        _linkRepository = linkRepository;
    }
    
    public async Task<EmptyResult> Handle(StartReadingBookRequest request)
    {
        Book book = await _bookRepository.FindByIdAsync(new BookId(request.BookId));
        if (book == null)
        {
            throw new Exception($"Cannot find book by id = {request.BookId}");
        }

        IReadOnlyList<Link> links = await _linkRepository.FindAllLinksFrom(book.Id.ToString());
        // если какие-то другие пользователи читали книгу, считаем, что мы передали её новому,
        // поэтому удалим существующие связи, кроме связи с текущем пользователем

        var currentLinkExists = false;
        foreach (Link link in links)
        {
            if (link.ToId == request.ReaderId)
            {
                currentLinkExists = true;
            }
            else
            {
                await _linkRepository.Delete(link);
            }
        }
        
        // создаём связь с текущим читателем, если это нужно
        if (!currentLinkExists)
        {
            var link = new Link(LinkId.CreateNew(), book.Id.ToString(), request.ReaderId);
            await _linkRepository.Add(link);
        }
        
        // теперь собственно открываем книгу
        book.OpenOnPage(request.PageToStart ?? 1);
        await _bookRepository.SaveChangesAsync(book);

        return new EmptyResult();
    }
}