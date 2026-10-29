using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Songs;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    //CONSTUCTOR
    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Song? Song { get; set; }
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

}
