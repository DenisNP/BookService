using BookService.Domain.Entities;
using BookService.Domain.ValueObjects;

namespace BookService.Application.Interfaces;

public interface IBookRepository
{
    public Task<Book> FindByIdAsync(BookId bookId);
    public Task SaveChangesAsync(Book book);
}