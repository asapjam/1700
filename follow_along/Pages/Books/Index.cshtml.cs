using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using follow_along.Data;
using follow_along.Models;

namespace follow_along.Pages.Books;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Book> Books { get; private set; } = new();

    public async Task OnGetAsync()
    {
        Books = await _context.Books
            .AsNoTracking()
            .Include(b => b.Category)
            .OrderBy(b => b.Title)
            .ToListAsync();
    }
}
