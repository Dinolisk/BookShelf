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
public class QuotesController(AppDbContext db) : ControllerBase
{
    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuoteResponse>>> GetAll()
    {
        var quotes = await db.Quotes
            .Where(q => q.UserId == CurrentUserId)
            .OrderByDescending(q => q.CreatedAt)
            .Select(q => new QuoteResponse(q.Id, q.Text, q.Author, q.Book))
            .ToListAsync();

        return Ok(quotes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuoteResponse>> GetById(int id)
    {
        var quote = await db.Quotes
            .FirstOrDefaultAsync(q => q.Id == id && q.UserId == CurrentUserId);
        if (quote is null)
            return NotFound();

        return Ok(new QuoteResponse(quote.Id, quote.Text, quote.Author, quote.Book));
    }

    [HttpPost]
    public async Task<ActionResult<QuoteResponse>> Create(QuoteRequest request)
    {
        var quote = new Quote
        {
            Text = request.Text,
            Author = Normalize(request.Author),
            Book = Normalize(request.Book),
            UserId = CurrentUserId,
        };

        db.Quotes.Add(quote);
        await db.SaveChangesAsync();

        var response = new QuoteResponse(quote.Id, quote.Text, quote.Author, quote.Book);
        return CreatedAtAction(nameof(GetById), new { id = quote.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, QuoteRequest request)
    {
        var quote = await db.Quotes
            .FirstOrDefaultAsync(q => q.Id == id && q.UserId == CurrentUserId);
        if (quote is null)
            return NotFound();

        quote.Text = request.Text;
        quote.Author = Normalize(request.Author);
        quote.Book = Normalize(request.Book);

        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var quote = await db.Quotes
            .FirstOrDefaultAsync(q => q.Id == id && q.UserId == CurrentUserId);
        if (quote is null)
            return NotFound();

        db.Quotes.Remove(quote);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private static string? Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
