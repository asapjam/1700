using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using follow_along.Data;
using follow_along.Models;

namespace follow_along.Pages.Books;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public List<Category> Categories { get; private set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required.")]
        [MaxLength(200)]
        public string Author { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
    }

    public async Task OnGetAsync()
    {
        await LoadCategoriesAsync();
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

        var book = new Book
        {
            Title = Input.Title.Trim(),
            Author = Input.Author.Trim(),
            CategoryId = Input.CategoryId
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Book created successfully!";

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