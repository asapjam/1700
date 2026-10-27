using Microsoft.EntityFrameworkCore;

using follow_along.Data;
using follow_along.Models;

namespace follow_along.Endpoints;

public class BookInput
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int CategoryId { get; set; }
}
public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        var books = app.MapGroup("/api/books");

        books.MapGet("", async (AppDbContext db) =>
        {
            var results = await db.Books
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            return Results.Ok(results);
        });

        books.MapGet("/{id:int}", async (int id, AppDbContext db) =>
        {
            var book = await db.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return book is not null
                ? Results.Ok(book)
                : Results.NotFound();
        });

        books.MapPost("", async (BookInput input, AppDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(input.Title) ||
                string.IsNullOrWhiteSpace(input.Author))
            {
                return Results.BadRequest(
                    new { error = "Title and Author are required." });
            }

            var categoryExists = await db.Categories
                .AnyAsync(c => c.Id == input.CategoryId);

            if (!categoryExists)
            {
                return Results.BadRequest(
                    new { error = "A valid category is required." });
            }

            var book = new Book
            {
                Title = input.Title.Trim(),
                Author = input.Author.Trim(),
                CategoryId = input.CategoryId
            };

            db.Books.Add(book);
            await db.SaveChangesAsync();

            return Results.Created($"/api/books/{book.Id}", book);
        });

        books.MapPut("/{id:int}", async (
            int id,
            BookInput input,
            AppDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(input.Title) ||
                string.IsNullOrWhiteSpace(input.Author))
            {
                return Results.BadRequest(
                    new { error = "Title and Author are required." });
            }

            var categoryExists = await db.Categories
                .AnyAsync(c => c.Id == input.CategoryId);

            if (!categoryExists)
            {
                return Results.BadRequest(
                    new { error = "A valid category is required." });
            }

            var book = await db.Books.FindAsync(id);

            if (book is null)
            {
                return Results.NotFound();
            }

            book.Title = input.Title.Trim();
            book.Author = input.Author.Trim();
            book.CategoryId = input.CategoryId;

            await db.SaveChangesAsync();

            return Results.Ok(book);
        });

        books.MapDelete("/{id:int}", async (int id, AppDbContext db) =>
        {
            var book = await db.Books.FindAsync(id);

            if (book is null)
            {
                return Results.NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}