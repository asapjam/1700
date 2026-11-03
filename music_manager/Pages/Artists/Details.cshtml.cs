using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Artists;

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

    public Artist? Artist { get; set; }
    public async Task<IActionResult> OnGetAsync(int Id)
    {
        try
        {
            Artist = await _context.Artists
                        .AsNoTracking()
                        .Include(a => a.Songs)
                        .Include(a => a.Albums)
                        .OrderBy(a => a.Name)
                        .FirstOrDefaultAsync(a => a.Id == Id);
            
            if(Artist is null)
            {
                _logger.LogWarning("Details page cannot display an Invalid Artist.Id: {Id}.", Id);
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the artist details.");
            ModelState.AddModelError(string.Empty, "An error occurred while retrieving the artist details. Please try again later.");
        }

        return Page();
    }

}
