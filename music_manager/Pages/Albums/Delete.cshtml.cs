using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Albums;

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

    public Album? Album { get; private set; }

    public async Task<IActionResult> OnGetAsync(int Id)
    {
        try
        {
            Album = await _context.Albums
            .AsNoTracking()
            .Include(s => s.Songs)
            .Include(s => s.Artist)
            .FirstOrDefaultAsync(s => s.Id == Id);

            if(Album is null)
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the album details for deletion.");
            ModelState.AddModelError(string.Empty, "An error occurred while retrieving the album details. Please try again later.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int Id)
    {
        try
        {
            Album = await _context.Albums
                    .AsNoTracking()
                    .Include(s => s.Songs)
                    .Include(s => s.Artist)
                    .FirstOrDefaultAsync(s => s.Id == Id);

            if(Album is null)
            {
                _logger.LogWarning("Delete attempted on non-existent album with Id: {Id}.", Id);
                return NotFound();
            }

            if (await DoesAlbumHaveSongs(Album)){
                _logger.LogWarning("Delete attempted on album Id: {Id} that still has associated songs.", Id);
                ModelState.AddModelError(string.Empty, "Cannot delete album because it has associated songs. Please delete the songs first.");
                return Page();
            }

            _context.Albums.Remove(Album);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Album with Id: {Id} was successfully deleted.", Id);
            TempData["StatusMessage"] = "Album deleted successfully!";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the album with Id: {Id}.", Id);
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the album. Please try again later.");
            return Page();
        }
    }

    public async Task<bool> DoesAlbumHaveSongs(Album album)
    {
        if(album.Songs.Count() != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

