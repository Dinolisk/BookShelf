using BookQuotesApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesApp.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Quote> Quotes => Set<Quote>();
}
