using BookService.Application.Interfaces;
using BookService.Application.RequestModels;
using BookService.Application.ViewModels;
using BookService.Domain.Entities;
using BookService.Domain.ValueObjects;

namespace BookService.Application.ApplicationServices;

public class StartReadingBookAgeDecorator : IApplicationService<StartReadingBookRequest, EmptyResult>
{
    private readonly IApplicationService<StartReadingBookRequest, EmptyResult> _applicationService;
    private readonly IUserRepository _userRepository;

    public StartReadingBookAgeDecorator(
        IApplicationService<StartReadingBookRequest, EmptyResult> applicationService, IUserRepository userRepository)
    {
        _applicationService = applicationService;
        _userRepository = userRepository;
    }
    
    public async Task<EmptyResult> Handle(StartReadingBookRequest request)
    {
        User user = await _userRepository.FindByIdAsync(new UserId(request.ReaderId));
        if (user == null)
        {
            throw new Exception($"No reader found by id = {request.ReaderId}");
        }

        if (user.Age < 12)
        {
            throw new Exception("Readers under 12 years old cannot read books");
        }

        return await _applicationService.Handle(request);
    }
}