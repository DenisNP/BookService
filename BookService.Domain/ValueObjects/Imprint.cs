namespace BookService.Domain.ValueObjects;

// объект-значение "Выходные данные" при публикации книги
public record Imprint
{
    public int Circulation { get; }
    public string Publisher { get; }
    public DateTime PublishDate { get; }

    public Imprint(int circulation, string publisher, DateTime publishDate)
    {
        if (circulation <= 0)
        {
            throw new Exception("Circulation could not be zero or less");
        }

        if (string.IsNullOrEmpty(publisher))
        {
            throw new Exception("Publisher cannot be empty");
        }

        Circulation = circulation;
        Publisher = publisher;
        PublishDate = publishDate;
    }
};