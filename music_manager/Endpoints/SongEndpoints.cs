using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Endpoints;

public class SongInput
{
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string Genre { get; set; } = string.Empty;

    // Each Song has one Artist.
    public int ArtistId { get; set; }
    public Artist? Artist { get; set; }
    // Each Song has one Album.
    public int AlbumId { get; set; }
    public Album? Album { get; set; }
}
public static class SongEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        var songs = app.MapGroup("/api/songs");

        songs.MapGet("", async (AppDbContext db) =>
        {
            var results = await db.Songs
                .AsNoTracking()
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Include(s => s.Album.Artist)
                .OrderBy(s => s.Title)
                .ToListAsync();

            return Results.Ok(results);
        });

        songs.MapGet("/{id:int}", async (int id, AppDbContext db) =>
        {
            var song = await db.Songs
                .AsNoTracking()
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Include(s => s.Album.Artist)
                .FirstOrDefaultAsync(b => b.Id == id);

            return song is not null
                ? Results.Ok(song)
                : Results.NotFound();
        });

        songs.MapPost("", async (SongInput input, AppDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(input.Title) ||
                string.IsNullOrWhiteSpace(input.Genre))
            {
                return Results.BadRequest(
                    new { error = "Title and Genre are required." });
            }
            
            if (input.ReleaseYear > 2026 || input.ReleaseYear < 1)
            {
                return Results.BadRequest(
                    new { error = "Invalid Release Year." });
            }

            var artistExists = await db.Artists
                .AnyAsync(c => c.Id == input.ArtistId);

            if (!artistExists)
            {
                return Results.BadRequest(
                    new { error = "A valid Artist is required." });
            }

            var albumExists = await db.Albums
                .AnyAsync(c => c.Id == input.AlbumId);

            if (!albumExists)
            {
                return Results.BadRequest(
                    new { error = "A valid Album is required." });
            }

            var album = await db.Albums
                .AsNoTracking()
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(a => a.Id == input.AlbumId);

            var artist = await db.Artists
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == input.ArtistId);

            var song = new Song
            {
                Title = input.Title.Trim(),
                ReleaseYear = input.ReleaseYear,
                Genre = input.Genre.Trim(),
                AlbumId = input.AlbumId,
                Album = album,
                ArtistId = input.ArtistId,
                Artist = artist

            };

            db.Songs.Add(song);
            await db.SaveChangesAsync();

            return Results.Created($"/api/songs/{song.Id}", song);
        });

        songs.MapPut("/{id:int}", async (
            int id,
            SongInput input,
            AppDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(input.Title) ||
                string.IsNullOrWhiteSpace(input.Genre))
            {
                return Results.BadRequest(
                    new { error = "Title and Genre are required." });
            }
            
            if (input.ReleaseYear > 2026 || input.ReleaseYear < 1)
            {
                return Results.BadRequest(
                    new { error = "Invalid Release Year." });
            }

            var artistExists = await db.Artists
                .AnyAsync(c => c.Id == input.ArtistId);

            if (!artistExists)
            {
                return Results.BadRequest(
                    new { error = "A valid Artist is required." });
            }

            var albumExists = await db.Albums
                .AnyAsync(c => c.Id == input.AlbumId);

            if (!albumExists)
            {
                return Results.BadRequest(
                    new { error = "A valid Album is required." });
            }

            var song = await db.Songs.FindAsync(id);

            if (song is null)
            {
                return Results.NotFound();
            }

            var album = await db.Albums
                .AsNoTracking()
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(a => a.Id == input.AlbumId);

            var artist = await db.Artists
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == input.ArtistId);

            song.Title = input.Title.Trim();
            song.ReleaseYear = input.ReleaseYear;
            song.Genre = input.Genre.Trim();
            song.AlbumId = input.AlbumId;
            song.Album = album;
            song.ArtistId = input.ArtistId;
            song.Artist = artist;

            await db.SaveChangesAsync();

            return Results.Ok(song);
        });

        songs.MapDelete("/{id:int}", async (int id, AppDbContext db) =>
        {
            var song = await db.Songs.FindAsync(id);

            if (song is null)
            {
                return Results.NotFound();
            }

            db.Songs.Remove(song);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}