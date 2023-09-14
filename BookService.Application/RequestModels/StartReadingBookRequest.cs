using System.ComponentModel.DataAnnotations;

namespace BookService.Application.RequestModels;

public record StartReadingBookRequest
{
    [Required]
    public string BookId { get; set; }
    [Required]
    public string ReaderId { get; set; }
    public int? PageToStart { get; set; }
}