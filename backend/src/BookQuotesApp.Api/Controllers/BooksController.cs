using System.Security.Claims;
using BookQuotesApp.Api.Data;
using BookQuotesApp.Api.Dtos;
using BookQuotesApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesApp.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BooksController(AppDbContext db) : ControllerBase
{
    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponse>>> GetAll()
    {
        var books = await db.Books
            .Where(b => b.UserId == CurrentUserId)
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new BookResponse(b.Id, b.Title, b.Author, b.PublishedDate, b.CoverImageUrl))
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookResponse>> GetById(int id)
    {
        var book = await db.Books
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == CurrentUserId);
        if (book is null)
            return NotFound();

        return Ok(new BookResponse(book.Id, book.Title, book.Author, book.PublishedDate, book.CoverImageUrl));
    }

    [HttpPost]
    public async Task<ActionResult<BookResponse>> Create(BookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            PublishedDate = request.PublishedDate,
            CoverImageUrl = Normalize(request.CoverImageUrl),
            UserId = CurrentUserId,
        };

        db.Books.Add(book);
        await db.SaveChangesAsync();

        var response = new BookResponse(book.Id, book.Title, book.Author, book.PublishedDate, book.CoverImageUrl);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, BookRequest request)
    {
        var book = await db.Books
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == CurrentUserId);
        if (book is null)
            return NotFound();

        book.Title = request.Title;
        book.Author = request.Author;
        book.PublishedDate = request.PublishedDate;
        book.CoverImageUrl = Normalize(request.CoverImageUrl);

        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await db.Books
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == CurrentUserId);
        if (book is null)
            return NotFound();

        db.Books.Remove(book);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private static string? Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
