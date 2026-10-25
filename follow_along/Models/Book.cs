namespace follow_along.Models;

public class Book
{
    public int Id { get; set; }           // Primary key by convention
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}
