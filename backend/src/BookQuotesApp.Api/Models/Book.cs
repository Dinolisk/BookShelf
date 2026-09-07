using System.ComponentModel.DataAnnotations;

namespace BookQuotesApp.Api.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Author { get; set; } = string.Empty;

    public DateOnly PublishedDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
