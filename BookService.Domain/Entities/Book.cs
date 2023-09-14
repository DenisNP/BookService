using BookService.Domain.Common;
using BookService.Domain.DomainEvents;
using BookService.Domain.ValueObjects;

namespace BookService.Domain.Entities;

public class Book : Entity<BookId>
{
    public string AuthorName { get; }
    public string Title { get; }
    public IReadOnlyList<string> Pages { get; private set; }
    public Imprint Imprint { get; private set; }

    public bool Opened { get; private set; }
    public bool Published { get; private set; }
    public int? CurrentPageNumber { get; private set; }
    public string? CurrentPage => CurrentPageNumber.HasValue ? Pages[CurrentPageNumber.Value - 1] : null;

    // мы хотим создавать разные типы книг снаружи и, чтобы не допустить ошибок, закрываем конструктор
    private Book(BookId id, string authorName, string title, IList<string> pages, Imprint imprint) : base(id)
    {
        if (string.IsNullOrEmpty(authorName))
        {
            throw new Exception($"{nameof(authorName)} cannot be empty");
        }

        if (string.IsNullOrEmpty(title))
        {
            throw new Exception($"{nameof(title)} cannot be empty");
        }

        if (pages == null)
        {
            throw new Exception($"Specify {nameof(pages)}");
        }

        // проверим, что ни одна страница не является null-ом
        // при этом страница без текста быть вполне может
        for (var i = 0; i < pages.Count; i++)
        {
            string p = pages[i];
            if (p == null)
            {
                throw new Exception($"Page with index {i} is null");
            }
        }

        AuthorName = authorName;
        Title = title;
        Pages = pages.AsReadOnly();
        Imprint = imprint;
        Opened = false;
        CurrentPageNumber = null;
        Published = imprint != null;
    }

    // в опубликованной книге обязательно наличие выходных данных (imprint),
    // поэтому подобная проверка дополнительно проводится в этом методе-фабрике
    public static Book CreateNewPublishedBook(string authorName, string title, IList<string> pages, Imprint imprint)
    {
        if (imprint == null)
        {
            throw new Exception("For published book imprint is required");
        }

        var id = BookId.CreateNew();
        return new Book(id, authorName, title, pages, imprint);
    }

    // если книга не была опубликована (назовём её "черновик"),
    // то выходных данных у неё ещё нет
    public static Book CreateNewDraftBook(string authorName, string title, IList<string> pages)
    {
        var id = BookId.CreateNew();
        return new Book(id, authorName, title, pages, null);
    }

    public void Publish(Imprint imprint)
    {
        Imprint = imprint ?? throw new Exception("For published book imprint is required");
        Published = true;
        
        RegisterEvent(new BookStateChangedDomainEvent(Id));
    }

    public void OpenOnPage(int pageNumber)
    {
        if (Pages.Count == 0)
        {
            throw new Exception("No pages to open on");
        }

        if (pageNumber <= 0 || pageNumber > Pages.Count)
        {
            throw new Exception("Invalid page number");
        }

        CurrentPageNumber = pageNumber;
        Opened = true;
        
        RegisterEvent(new BookStateChangedDomainEvent(Id));
    }

    public void Close()
    {
        CurrentPageNumber = null;
        Opened = false;
    }

    // метод, вырывающий страницу из книги, необратимо меняет её состояние
    public void TearCurrentPageOut()
    {
        if (Pages.Count == 0)
        {
            throw new Exception("No pages to tear");
        }

        if (!Opened)
        {
            throw new Exception("Open the book on some page to tear page out");
        }

        Pages = Pages.Take(..(CurrentPageNumber!.Value - 1))
            .Concat(Pages.Take(CurrentPageNumber!.Value..))
            .ToList()
            .AsReadOnly();

        if (CurrentPageNumber.Value > Pages.Count)
        {
            CurrentPageNumber--; // если мы вырвали последнюю страницу, то предыдущая становится текущей
            if (CurrentPageNumber <= 0)
            {
                // в книге была последняя страница, и мы её вырвали
                // конкретно в нашей интерпретации книга теперь не может быть открыта на какой-то странице,
                // закрываем её
                Close();
            }
        }
        
        RegisterEvent(new BookStateChangedDomainEvent(Id));
    }
}