using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using follow_along.Data;
using follow_along.Models;

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

    public List<Category> Categories { get; private set; } = new();

    public class InputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required.")]
        [MaxLength(200)]
        public string Author { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
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
            Author = book.Author,
            CategoryId = book.CategoryId
        };

        await LoadCategoriesAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!await CategoryExistsAsync(Input.CategoryId))
        {
            ModelState.AddModelError(
                "Input.CategoryId",
                "Please select a valid category.");
        }

        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return Page();
        }

        var book = await _context.Books.FindAsync(Input.Id);

        if (book is null)
        {
            return NotFound();
        }

        book.Title = Input.Title.Trim();
        book.Author = Input.Author.Trim();
        book.CategoryId = Input.CategoryId;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private async Task LoadCategoriesAsync()
    {
        Categories = await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    private async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _context.Categories
            .AnyAsync(c => c.Id == categoryId);
    }
}