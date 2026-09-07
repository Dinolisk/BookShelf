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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponse>>> GetAll()
    {
        var books = await db.Books
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new BookResponse(b.Id, b.Title, b.Author, b.PublishedDate))
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookResponse>> GetById(int id)
    {
        var book = await db.Books.FindAsync(id);
        if (book is null)
            return NotFound();

        return Ok(new BookResponse(book.Id, book.Title, book.Author, book.PublishedDate));
    }

    [HttpPost]
    public async Task<ActionResult<BookResponse>> Create(BookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            PublishedDate = request.PublishedDate,
        };

        db.Books.Add(book);
        await db.SaveChangesAsync();

        var response = new BookResponse(book.Id, book.Title, book.Author, book.PublishedDate);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, BookRequest request)
    {
        var book = await db.Books.FindAsync(id);
        if (book is null)
            return NotFound();

        book.Title = request.Title;
        book.Author = request.Author;
        book.PublishedDate = request.PublishedDate;

        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await db.Books.FindAsync(id);
        if (book is null)
            return NotFound();

        db.Books.Remove(book);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
