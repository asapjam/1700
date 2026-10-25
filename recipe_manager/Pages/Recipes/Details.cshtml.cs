using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using recipe_manager.Data;
using recipe_manager.Models;

namespace recipe_manager.Pages.Recipes;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty (SupportsGet = true)]
    public int Id {get; set;}

    public Recipe? recipe;

    public async Task OnGetAsync()
    {
        recipe = await _context.Recipes
            .AsNoTracking()
            .Where(x => x.Id == Id)
            .FirstOrDefaultAsync();
    }
}