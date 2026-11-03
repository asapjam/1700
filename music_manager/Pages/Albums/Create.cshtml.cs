using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Albums;

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
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Release Year is required.")]
        [Range(0, 2026)]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Artist is required.")]
        [Range(1, int.MaxValue)]
        public int ArtistId { get; set; }
    }

    //A new InputModel is created, and the form is bound to it- to prevent overposting
    [BindProperty]
    public InputModel Input { get; set; } = new();

    //Pull Artists from the database
    public List<Artist> Artists = new();

    public async Task OnGetAsync()
    {
        try
        {
            GrabAllArtists();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving artists.");
            ModelState.AddModelError(string.Empty, "An error occurred while retrieving artists. Please try again later.");
        }
    }

    private async void GrabAllArtists()
    {
        Artists = await _context.Artists
            .AsNoTracking()
            .ToListAsync();
    }

    //Validate form, use the InputModel Object and its bound properties to make a Recipe Object and add it to our DbContext which writes it to the SQLite database
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!await ArtistExistsAsync(Input.ArtistId))
            {
                ModelState.AddModelError(
                    "Input.ArtistId",
                    "Please select a valid artist.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var album = new Album
            {
                Name = Input.Name.Trim(),
                ReleaseYear = Input.ReleaseYear,
                ArtistId = Input.ArtistId
            };

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Album created successfully: {AlbumName} (ID: {AlbumId})", album.Name, album.Id);
            TempData["StatusMessage"] = "Album created successfully!";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the album.");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the album. Please try again later.");
            return Page();
        }
    }

    //Validation helper functions
    private async Task<bool> ArtistExistsAsync(int artistId)
    {
        return await _context.Artists
            .AnyAsync(c => c.Id == artistId);
    }

}

