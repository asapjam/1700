using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Songs;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<DeleteModel> _logger;
    
    // CONSTRUCTOR W DOOR TO DATABASE
    public DeleteModel(AppDbContext context, ILogger<DeleteModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Song? Song { get; private set; }

    public async Task<IActionResult> OnGetAsync(int Id)
    {
        Song = await _context.Songs
                    .AsNoTracking()
                    .Include(s => s.Album)
                    .Include(s => s.Artist)
                    .FirstOrDefaultAsync(s => s.Id == Id);

        if(Song is null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int Id)
    {
        try
        {
            var song = await _context.Songs.FindAsync(Id);

            if(song is null)
            {
                _logger.LogWarning("Delete attempted on non-existent song with Id: {Id}.", Id);
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Song with Id: {Id} was successfully deleted.", Id);
            TempData["StatusMessage"] = "Song deleted successfully!";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the song with Id: {Id}.", Id);
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the song. Please try again later.");
            return Page();
        }
    }
}

