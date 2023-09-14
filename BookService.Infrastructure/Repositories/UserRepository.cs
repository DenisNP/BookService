using BookService.Application.Interfaces;
using BookService.Domain.Entities;
using BookService.Domain.ValueObjects;

namespace BookService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task<User> FindByIdAsync(UserId userId)
    {
        throw new NotImplementedException();
    }
}