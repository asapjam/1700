using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using follow_along.Data;
using follow_along.Models;

namespace follow_along.Pages.Books;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
    {
        _context = context;
    }

    public Book? Book { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Book = await _context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (Book is null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Book deleted successfully!";

        return RedirectToPage("./Index");
    }
}