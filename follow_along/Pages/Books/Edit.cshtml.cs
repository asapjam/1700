using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using follow_along.Data;

namespace follow_along.Pages.Books;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required.")]
        [MaxLength(200)]
        public string Author { get; set; } = string.Empty;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var book = await _context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null)
        {
            return NotFound();
        }

        Input = new InputModel
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var book = await _context.Books.FindAsync(Input.Id);

        if (book is null)
        {
            return NotFound();
        }

        book.Title = Input.Title.Trim();
        book.Author = Input.Author.Trim();

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}