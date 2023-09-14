using BookService.Domain.Common;
using BookService.Domain.ValueObjects;

namespace BookService.Domain.Entities;

public class User : Entity<UserId>
{
    public DateTime Birthdate { get; }
    public string Name { get; }

    // в реальном мире проверка, разумеется, сложнее,
    // и должна учитывать, прошёл ли день рождения пользователя в этом году
    public int Age => DateTime.Now.Year - Birthdate.Year;

    // у пользователя нет различных вариаций создания, так что открываем конструктор
    public User(UserId id, DateTime birthdate, string name) : base(id)
    {
        if (birthdate > DateTime.Now)
        {
            throw new Exception("Birthdate cannot be in future");
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new Exception($"{nameof(name)} cannot be empty");
        }

        Birthdate = birthdate;
        Name = name;
    }
}