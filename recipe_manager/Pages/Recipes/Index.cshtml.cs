using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using recipe_manager.Data;
using recipe_manager.Models;

namespace recipe_manager.Pages.Recipes;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Recipe> Recipes { get; private set; } = new();

    public async Task OnGetAsync()
    {
        Recipes = await _context.Recipes
            .AsNoTracking()
            .OrderBy(b => b.Name)
            .ToListAsync();
    }
}