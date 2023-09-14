using BookService.Application.ApplicationServices;
using BookService.Application.Interfaces;
using BookService.Application.RequestModels;
using BookService.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BookService.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IApplicationService<GetBookCardRequest, BookCardViewModel>, GetBookCardApplicationService>()

            // к этому сервису добавим декоратор, который проверит возраст читателя
            .AddScoped<IApplicationService<StartReadingBookRequest, EmptyResult>, StartReadingBookApplicationService>()
            .Decorate<IApplicationService<StartReadingBookRequest, EmptyResult>, StartReadingBookAgeDecorator>();
    }
}