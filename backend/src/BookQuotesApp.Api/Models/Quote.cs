using System.ComponentModel.DataAnnotations;

namespace BookQuotesApp.Api.Models;

public class Quote
{
    public int Id { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Text { get; set; } = string.Empty;

    /// <summary>Who said or wrote the quote.</summary>
    [MaxLength(200)]
    public string? Author { get; set; }

    /// <summary>The book the quote is from (optional).</summary>
    [MaxLength(200)]
    public string? Book { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Each quote belongs to the user who created it.
    public int UserId { get; set; }
    public User? User { get; set; }
}
