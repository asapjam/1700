using System;
using System.ComponentModel.DataAnnotations;

namespace recipe_manager.Models;

public class Recipe
{
    public int Id {get; set;}

    [Required]
    [MaxLength(100)]
    public string Name {get; set;}

    [MaxLength(500)]
    public string? Description {get; set;}

    [Required]
    [MinLength(10)]
    public string Ingredients {get; set;}
    
    [Required]
    [MinLength(10)]
    public string Instructions {get; set;}
}
