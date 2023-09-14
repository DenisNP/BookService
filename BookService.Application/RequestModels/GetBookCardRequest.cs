using System.ComponentModel.DataAnnotations;

namespace BookService.Application.RequestModels;

public record GetBookCardRequest
{
    [Required]
    public string BookId { get; set; }
    public bool IncludeUnpublished { get; set; }
};