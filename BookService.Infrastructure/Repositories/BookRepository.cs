using BookService.Application.Interfaces;
using BookService.Domain.Common;
using BookService.Domain.Entities;
using BookService.Domain.ValueObjects;

namespace BookService.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDomainEventVisitor _visitor;

    public BookRepository(IDomainEventVisitor visitor)
    {
        _visitor = visitor;
    }

    public Task<Book> FindByIdAsync(BookId bookId)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(Book book)
    {
        // сначала вызываем commit transaction
        // _dbContext.CommitTransaction...
        
        // по идее нужен отдельный объект RequestContext, который начинает и заканчивает
        // транзакции, потому что это действие не должно быть привязано к записи в конкретный
        // репозиторий, но здесь мы приводим пример для упрощения

        // затем обрабатываем доменные события
        // в реальной жизни можно сложить из в очередь и обработать в фоне,
        // не забыв при этом создать отдельный ServiceScope,
        // не связанный с запросом текущего пользователя
        foreach (IDomainEvent domainEvent in book.Events)
        {
            domainEvent.Accept(_visitor);
        }

        throw new NotImplementedException();
    }
}