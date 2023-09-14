using BookService.Application.Interfaces;
using BookService.Application.RequestModels;
using BookService.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using EmptyResult = BookService.Application.ViewModels.EmptyResult;

namespace BookService.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public BookController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    [HttpGet("GetBookCard")]
    public async Task<BookCardViewModel> GetBookCardAsync(GetBookCardRequest request)
    {
        return await _serviceProvider
            .GetRequiredService<IApplicationService<GetBookCardRequest, BookCardViewModel>>()
            .Handle(request);
    }

    [HttpPost("StartReadingBook")]
    public async Task<EmptyResult> StartReadingBookAsync(StartReadingBookRequest request)
    {
        return await _serviceProvider
            .GetRequiredService<IApplicationService<StartReadingBookRequest, EmptyResult>>()
            .Handle(request);
    }
}