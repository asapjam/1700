using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using recipe_manager.Data;
using recipe_manager.Models;

namespace recipe_manager.Pages.Recipes;


public class CreateModel : PageModel
{
    //DbContext allows us to chatter w/ database
    private readonly AppDbContext _context;

    //Constructor makes a disposable DbContext automatically?
    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    //Define what parameters nested InputModel will accept
    public class InputModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingredients are required.")]
        [MinLength(10)]
        public string Ingredients { get; set; } = string.Empty;

        [Required(ErrorMessage = "Instructions are required.")]
        [MinLength(10)]
        public string Instructions { get; set; } = string.Empty;
    }
    
    //A new InputModel is created, and the form is bound to it- to prevent overposting
    [BindProperty]
    public InputModel Input { get; set; } = new();

    public void OnGet()
    {
        // Render the empty form
    }

    //Validate form, use the InputModel Object and its bound properties to make a Recipe Object and add it to our DbContext which writes it to the SQLite database
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var recipe = new Recipe
        {
            Name = Input.Name.Trim(),
            Description = Input.Description != null ? Input.Description.Trim() : string.Empty,
            Ingredients = Input.Ingredients.Trim(),
            Instructions = Input.Instructions.Trim()
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();


        return RedirectToPage("./Details", recipe);
    }
}

