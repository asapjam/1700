using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Songs;

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

    public Song? Song { get; set; }
    public async Task<IActionResult> OnGetAsync(int Id)
    {
        try
        {
            Song = await _context.Songs
                    .AsNoTracking()
                    .Include(s => s.Album)
                    .Include(s => s.Artist)
                    .FirstOrDefaultAsync(s => s.Id == Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while retrieving the song details.");
            ModelState.AddModelError(string.Empty, "An error occurred while retrieving the song details. Please try again later.");
        }
        
        if(Song is null)
        {
            _logger.LogWarning("Details page cannot display an Invalid Song.Id: {Id}.", Id);
            return NotFound();
        }

        return Page();
    }

}
