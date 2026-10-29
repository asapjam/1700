using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Albums;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    //CONSTUCTOR
    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Album? Album { get; set; }
    public async Task<IActionResult> OnGetAsync(int Id)
    {
        Album = await _context.Albums
                    .AsNoTracking()
                    .Include(a => a.Songs)
                    .Include(a => a.Artist)
                    .OrderBy(a => a.Name)
                    .FirstOrDefaultAsync(a => a.Id == Id);
        
        if(Album is null)
        {
            return NotFound();
        }

        return Page();
    }

}
