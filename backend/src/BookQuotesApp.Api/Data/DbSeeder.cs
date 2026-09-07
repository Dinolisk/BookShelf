using BookQuotesApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesApp.Api.Data;

/// <summary>Seeds a handful of well-known books the first time the app runs against an empty database.</summary>
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Books.AnyAsync())
        {
            return;
        }

        db.Books.AddRange(
            new Book { Title = "1984", Author = "George Orwell", PublishedDate = new DateOnly(1949, 6, 8) },
            new Book { Title = "Sagan om ringen", Author = "J.R.R. Tolkien", PublishedDate = new DateOnly(1954, 7, 29) },
            new Book { Title = "Stolthet och fördom", Author = "Jane Austen", PublishedDate = new DateOnly(1813, 1, 28) },
            new Book { Title = "Brott och straff", Author = "Fjodor Dostojevskij", PublishedDate = new DateOnly(1866, 1, 1) },
            new Book { Title = "Fahrenheit 451", Author = "Ray Bradbury", PublishedDate = new DateOnly(1953, 10, 19) },
            new Book { Title = "Den store Gatsby", Author = "F. Scott Fitzgerald", PublishedDate = new DateOnly(1925, 4, 10) },
            new Book { Title = "Att döda en härmtrast", Author = "Harper Lee", PublishedDate = new DateOnly(1960, 7, 11) },
            new Book { Title = "Mästaren och Margarita", Author = "Michail Bulgakov", PublishedDate = new DateOnly(1967, 1, 1) });

        await db.SaveChangesAsync();
    }
}
