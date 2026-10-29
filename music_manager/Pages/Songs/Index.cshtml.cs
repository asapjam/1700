using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Songs
{
    public class IndexModel : PageModel
    {
        // Create an AppDbContext, so we have a disposable doorway to the Database
        private readonly AppDbContext _context;

        // Define Constructor, which takes a AppDbContext, so we can communicate w/ DB
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        // Define an empty List of Albums
        public List<Song> Songs { get; private set; } = new();

        // Fill that list with the existing entries in the Database.
        public async Task OnGetAsync()
        {
            Songs = await _context.Songs
                .AsNoTracking()
                .Include(b => b.Album)
                .Include(b => b.Artist)
                .OrderBy(b => b.Title)
                .ToListAsync();
        }

    }
}
