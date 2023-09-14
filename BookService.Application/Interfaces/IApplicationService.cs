namespace BookService.Application.Interfaces;

public interface IApplicationService<in TRequest, TResponse>
{
    public Task<TResponse> Handle(TRequest request);
}