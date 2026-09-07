using System.ComponentModel.DataAnnotations;

namespace BookQuotesApp.Api.Dtos;

public record BookResponse(int Id, string Title, string Author, DateOnly PublishedDate);

public class BookRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Author { get; set; } = string.Empty;

    [Required]
    public DateOnly PublishedDate { get; set; }
}
