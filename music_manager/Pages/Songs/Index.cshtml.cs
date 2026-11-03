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
        private readonly ILogger<EditModel> _logger;

        // Define Constructor, which takes a AppDbContext, so we can communicate w/ DB
        public IndexModel(AppDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Search functionality
        [BindProperty(SupportsGet = true)]
        public string? SongSearchTerm { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int? SearchAlbumId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchArtistId { get; set; }

        public List<Artist> AllArtists { get; set; } = new();
        public List<Album> AllAlbums { get; set; } = new();

        // Define an empty List of Albums
        public List<Song> Songs { get; private set; } = new();

        // Fill that list with the existing entries in the Database.
        public async Task<IActionResult> OnGetAsync(){
            await GetArtists();
            await GetAlbums();
            await SearchSongs();
            return Page();
        }

        public async Task GetArtists()
        {
            try{
                AllArtists = await _context.Artists.ToListAsync();
            } catch (Exception ex){
                _logger.LogError(ex, "An error occurred while retrieving artists.");
                ModelState.AddModelError(string.Empty, "An error occurred while searching for songs. Please try again later.");
            }
        }

        public async Task GetAlbums()
        {
            try{
                AllAlbums = await _context.Albums.ToListAsync();
            } catch (Exception ex){
                _logger.LogError(ex, "An error occurred while retrieving albums.");
                ModelState.AddModelError(string.Empty, "An error occurred while searching for songs. Please try again later.");
            }
        }

        public async Task SearchSongs()
        {
            try{
                var query = _context.Songs
                .Include(b => b.Artist)
                .Include(b => b.Album)
                .AsQueryable();

                if (!string.IsNullOrWhiteSpace(SongSearchTerm))
                {
                    query = query.Where(b =>
                        b.Title.ToLower().Contains(SongSearchTerm.ToLower()));
                }

                if (SearchArtistId.HasValue)
                {
                    query = query.Where(b => b.ArtistId == SearchArtistId.Value);
                }

                if (SearchAlbumId.HasValue)
                {
                    query = query.Where(b => b.AlbumId == SearchAlbumId.Value);
                }

                Songs = await query.OrderBy(b => b.Title).ToListAsync();
            } catch (Exception ex){
                _logger.LogError(ex, "An error occurred while searching for songs.");
                ModelState.AddModelError(string.Empty, "An error occurred while searching for songs. Please try again later.");
            }
        }

    }
}
