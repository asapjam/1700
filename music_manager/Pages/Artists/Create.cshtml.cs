using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using music_manager.Data;
using music_manager.Models;

namespace music_manager.Pages.Artists;
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
    }
    
    //A new InputModel is created, and the form is bound to it- to prevent overposting
    [BindProperty]
    public InputModel Input { get; set; } = new();


    //Validate form, use the InputModel Object and its bound properties to make a Recipe Object and add it to our DbContext which writes it to the SQLite database
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var artist = new Artist
        {
            Name = Input.Name.Trim(),
        };

        _context.Artists.Add(artist);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Artist created successfully!";

        return RedirectToPage("./Index");
    }
}

