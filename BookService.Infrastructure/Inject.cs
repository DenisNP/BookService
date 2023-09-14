using BookService.Application.Interfaces;
using BookService.Domain.Common;
using BookService.Infrastructure.QuerySources;
using BookService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookService.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ILinkRepository, LinkRepository>()
            .AddScoped<IBookCardQuerySource, BookCardQuerySource>()
            .AddScoped<IDomainEventVisitor, BookStateChangedVisitor>();
    }
}