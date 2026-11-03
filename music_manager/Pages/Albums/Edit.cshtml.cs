using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Albums;
public class EditModel : PageModel
{
    //DbContext allows us to chatter w/ database
    private readonly AppDbContext _context;
    private readonly ILogger<EditModel> _logger;

    //Constructor makes a disposable DbContext automatically?
    public EditModel(AppDbContext context, ILogger<EditModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    //Define what parameters nested InputModel will accept
    public class InputModel
    {
        public int Id { get; set; }

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
    public List<Artist> Artists { get; set; } = new();
 
    public async Task<IActionResult> OnGetAsync(int Id)
    {
        try
        {
            GrabAllArtists();

            var album = await _context.Albums
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (album is null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                Id = album.Id,
                Name = album.Name,
                ReleaseYear = album.ReleaseYear,
                ArtistId = album.ArtistId
            };
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the album for editing with Id: {Id}.", Id);
            ModelState.AddModelError(string.Empty, "An error occurred while retrieving the album for editing. Please try again later.");
        }
        return Page();
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

            var album = await _context.Albums.FindAsync(Input.Id);

            if (album is null)
            {
                _logger.LogWarning("Edit attempted on non-existent song with Id: {Id}.", Input.Id);
                return NotFound();
            }

            album.Name = Input.Name.Trim();
            album.ReleaseYear = Input.ReleaseYear;
            album.ArtistId = Input.ArtistId;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Album with Id: {Id} was successfully edited.", Input.Id);
            TempData["StatusMessage"] = "Album edited successfully!";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while committing edited album with Id: {Id} to the database.", Input.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while editing the album. Please try again later.");
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

