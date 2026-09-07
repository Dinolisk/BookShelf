using System.ComponentModel.DataAnnotations;

namespace BookQuotesApp.Api.Dtos;

public record QuoteResponse(int Id, string Text, string? Author, string? Book);

public class QuoteRequest
{
    [Required]
    [MaxLength(1000)]
    public string Text { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Author { get; set; }

    [MaxLength(200)]
    public string? Book { get; set; }
}
