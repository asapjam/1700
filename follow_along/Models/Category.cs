using System.Text.Json.Serialization;
namespace follow_along.Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Book> Books { get; set; } = new();
}