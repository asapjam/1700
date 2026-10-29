using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Songs;
public class EditModel : PageModel
{
    //DbContext allows us to chatter w/ database
    private readonly AppDbContext _context;

    //Constructor makes a disposable DbContext automatically?
    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    //Define what parameters nested InputModel will accept
    public class InputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Release Year is required.")]
        [Range(0, 2026)]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [MaxLength(100)]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Album is required.")]
        [Range(1, int.MaxValue)]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Artist is required.")]
        [Range(1, int.MaxValue)]
        public int ArtistId { get; set; }
    }
    
    //A new InputModel is created, and the form is bound to it- to prevent overposting
    [BindProperty]
    public InputModel Input { get; set; } = new();

    //Pull Artists from the database
    public List<Artist> Artists { get; set; } = new();

    //Pull Albums from the database
    public List<Album> Albums { get; set; } = new();
 
    public async Task<IActionResult> OnGetAsync(int Id)
    {
        GrabAllArtists();
        GrabAllAlbums();

        var song = await _context.Songs
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == Id);

        Console.WriteLine("OnGet song:");
        Console.WriteLine(song.Id);

        if (song is null)
        {
            return NotFound();
        }

        Input = new InputModel
        {
            Id = song.Id,
            Title = song.Title,
            ReleaseYear = song.ReleaseYear,
            Genre = song.Genre,
            AlbumId = song.AlbumId,
            ArtistId = song.ArtistId
        };

        Console.WriteLine("OnGet Input.Id:");
        Console.WriteLine(Input.Id);

        return Page();
    }

    private async void GrabAllArtists()
    {
        Artists = await _context.Artists
            .AsNoTracking()
            .ToListAsync();   
    }

    private async void GrabAllAlbums()
    {
        Albums = await _context.Albums
            .AsNoTracking()
            .ToListAsync();   
    }

    //Validate form, use the InputModel Object and its bound properties to make a Recipe Object and add it to our DbContext which writes it to the SQLite database
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!await ArtistExistsAsync(Input.ArtistId))
        {
            ModelState.AddModelError(
                "Input.ArtistId",
                "Please select a valid artist.");
        }

        if (!await AlbumExistsAsync(Input.AlbumId))
        {
            ModelState.AddModelError(
                "Input.AlbumId",
                "Please select a valid album.");
        }

        var song = await _context.Songs.FindAsync(Input.Id);

        Console.WriteLine("Input ReleaseYear");
        Console.WriteLine(Input.ReleaseYear);
        Console.WriteLine("song");
        Console.WriteLine(song);

        if (song is null)
        {
            Console.WriteLine("Song is null...");
            return NotFound();
        }

        song.Title = Input.Title.Trim();
        song.ReleaseYear = Input.ReleaseYear;
        song.Genre = Input.Genre.Trim();
        song.AlbumId = Input.AlbumId;
        song.ArtistId = Input.ArtistId;

        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Song edited successfully!";

        return RedirectToPage("./Index");
    }

    //Validation helper functions
    private async Task<bool> ArtistExistsAsync(int artistId)
    {
        return await _context.Artists
            .AnyAsync(c => c.Id == artistId);
    }

    private async Task<bool> AlbumExistsAsync(int albumId)
    {
        return await _context.Albums
            .AnyAsync(c => c.Id == albumId);
    }

}

