namespace BookService.Application.ViewModels;

// материализованное представление "Карточка книги" в котором, кроме данных самой книги,
// хранится ещё и информация о пользователе, который прямо сейчас эту книгу читает
public record BookCardViewModel
{
    public string Id { get; set; }
    public string AuthorName { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public bool IsPublished { get; set; }
    public BookStateViewModel State { get; set; }
}

public record UserViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

public record BookStateViewModel
{
    public bool IsOpened { get; set; }
    public string? CurrentPage { get; set; }
    public UserViewModel CurrentReader { get; set; }
}