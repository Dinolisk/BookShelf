using System.ComponentModel.DataAnnotations;

namespace BookQuotesApp.Api.Models;

public class Quote
{
    public int Id { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Text { get; set; } = string.Empty;

    /// <summary>Who said or wrote the quote (person or book).</summary>
    [MaxLength(200)]
    public string? Author { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Owner is added in the auth step (each user manages their own quotes).
}
