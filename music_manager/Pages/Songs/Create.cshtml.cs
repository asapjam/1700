using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Songs;
public class CreateModel : PageModel
{
    //DbContext allows us to chatter w/ database
    private readonly AppDbContext _context;
    private readonly ILogger<CreateModel> _logger;

    //Constructor makes a disposable DbContext automatically?
    public CreateModel(AppDbContext context, ILogger<CreateModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    //Define what parameters nested InputModel will accept
    public class InputModel
    {
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
    public List<Artist> Artists = new();

    //Pull Albums from the database
    public List<Album> Albums = new();
 
    public async Task OnGetAsync()
    {
        try
        {
            GrabAllArtists();
            GrabAllAlbums();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving artists and albums for the create song page.");
            ModelState.AddModelError(string.Empty, "An error occurred while retrieving artists and albums. Please try again later.");
        }
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

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var song = new Song
        {
            Title = Input.Title.Trim(),
            ReleaseYear = Input.ReleaseYear,
            Genre = Input.Genre.Trim(),
            AlbumId = Input.AlbumId,
            ArtistId = Input.ArtistId
        };

        try
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            _logger.LogInformation("New Song created with Id: {Id}.", song.Id);
            TempData["StatusMessage"] = "Song created successfully!";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new song.");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the song. Please try again later.");
            return Page();
        }
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

