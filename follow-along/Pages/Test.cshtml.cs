using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;

namespace follow_along.Pages.Pages;

public class TestModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "Message is required.")]
    public string Message { get; set; } = string.Empty;

    public void OnGet() {
        // Runs on initial GET /Test
    }
    public IActionResult OnPost() {
        // Runs when the form is submitted with POST
        // Model binding binds the posted form value to Message automatically
        if (!ModelState.IsValid) {
            return Page();
        }

        // If valid, the Message property is ready to use
        return Page();
    }
}
