using BookQuotesApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesApp.Api.Data;

/// <summary>Seeds a set of well-known books the first time the app runs against an empty database.</summary>
public static class DbSeeder
{
    /// <summary>
    /// Open Library serves free cover images keyed by ISBN, no API key required.
    /// `default=false` makes it return 404 (instead of a blank 1px image) when it has
    /// no cover, so the UI falls back to its placeholder.
    /// </summary>
    private static string Cover(string isbn) =>
        $"https://covers.openlibrary.org/b/isbn/{isbn}-L.jpg?default=false";

    private static readonly (string Title, string Author, int Year, string Isbn)[] Seed =
    [
        ("1984", "George Orwell", 1949, "9780451524935"),
        ("Djurfarmen", "George Orwell", 1945, "9780451526342"),
        ("Fahrenheit 451", "Ray Bradbury", 1953, "9781451673319"),
        ("Du sköna nya värld", "Aldous Huxley", 1932, "9780060850524"),
        ("Den store Gatsby", "F. Scott Fitzgerald", 1925, "9780743273565"),
        ("Att döda en härmtrast", "Harper Lee", 1960, "9780061120084"),
        ("Stolthet och fördom", "Jane Austen", 1813, "9780141439518"),
        ("Jane Eyre", "Charlotte Brontë", 1847, "9780142437209"),
        ("Svindlande höjder", "Emily Brontë", 1847, "9780141439556"),
        ("Brott och straff", "Fjodor Dostojevskij", 1866, "9780143058144"),
        ("Bröderna Karamazov", "Fjodor Dostojevskij", 1880, "9780374528379"),
        ("Anna Karenina", "Lev Tolstoj", 1878, "9780143035008"),
        ("Krig och fred", "Lev Tolstoj", 1869, "9781400079988"),
        ("Bilbo – En hobbits äventyr", "J.R.R. Tolkien", 1937, "9780547928227"),
        ("Sagan om ringen", "J.R.R. Tolkien", 1954, "9780544003415"),
        ("Mästaren och Margarita", "Michail Bulgakov", 1967, "9780143108276"),
        ("Hundra år av ensamhet", "Gabriel García Márquez", 1967, "9780060883287"),
        ("Frankenstein", "Mary Shelley", 1818, "9780141439471"),
        ("Dracula", "Bram Stoker", 1897, "9780141439846"),
        ("Dorian Grays porträtt", "Oscar Wilde", 1890, "9780141439570"),
        ("Moby Dick", "Herman Melville", 1851, "9780142437247"),
        ("Räddaren i nöden", "J.D. Salinger", 1951, "9780316769488"),
        ("Slakthus 5", "Kurt Vonnegut", 1969, "9780385333849"),
        ("Moment 22", "Joseph Heller", 1961, "9781451626650"),
        ("Processen", "Franz Kafka", 1925, "9780805209990"),
        ("Don Quijote", "Miguel de Cervantes", 1605, "9780060934347"),
    ];

    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Books.AnyAsync())
        {
            return;
        }

        db.Books.AddRange(Seed.Select(b => new Book
        {
            Title = b.Title,
            Author = b.Author,
            PublishedDate = new DateOnly(b.Year, 1, 1),
            CoverImageUrl = Cover(b.Isbn),
        }));

        await db.SaveChangesAsync();
    }

    /// <summary>Five favourite quotes given to every new account so the "Mina citat" view starts populated.</summary>
    public static List<Quote> StarterQuotes() =>
    [
        new() { Text = "Not all those who wander are lost.", Author = "J.R.R. Tolkien" },
        new()
        {
            Text = "It is a truth universally acknowledged, that a single man in possession of a good "
                   + "fortune, must be in want of a wife.",
            Author = "Jane Austen",
        },
        new() { Text = "So it goes.", Author = "Kurt Vonnegut" },
        new() { Text = "Whatever our souls are made of, his and mine are the same.", Author = "Emily Brontë" },
        new() { Text = "The only way to get rid of a temptation is to yield to it.", Author = "Oscar Wilde" },
    ];
}
