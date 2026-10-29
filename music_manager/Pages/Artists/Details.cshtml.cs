using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Artists;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    //CONSTUCTOR
    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Artist? Artist { get; set; }
    public async Task<IActionResult> OnGetAsync(int Id)
    {
        Artist = await _context.Artists
                    .AsNoTracking()
                    .Include(a => a.Songs)
                    .Include(a => a.Albums)
                    .OrderBy(a => a.Name)
                    .FirstOrDefaultAsync(a => a.Id == Id);
        
        if(Artist is null)
        {
            return NotFound();
        }

        return Page();
    }

}
