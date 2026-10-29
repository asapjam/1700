using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Songs;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;
    
    // CONSTRUCTOR W DOOR TO DATABASE
    public DeleteModel(AppDbContext context)
    {
        _context = context;
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
        var song = await _context.Songs.FindAsync(Id);

        if(song is null)
        {
            return NotFound();
        }

        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Song deleted successfully!";

        return RedirectToPage("./Index");
    }
}

