using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Albums;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<DetailsModel> _logger;

    //CONSTUCTOR
    public DetailsModel(AppDbContext context, ILogger<DetailsModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Album? Album { get; set; }
    public async Task<IActionResult> OnGetAsync(int Id)
    {
        try
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
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the album details.");
            ModelState.AddModelError(string.Empty, "An error occurred while retrieving the album details. Please try again later.");
        }
        return Page();
    }

}
