using BookService.Domain.Entities;
using BookService.Domain.ValueObjects;

namespace BookService.Application.Interfaces;

public interface IUserRepository
{
    public Task<User> FindByIdAsync(UserId userId);
}