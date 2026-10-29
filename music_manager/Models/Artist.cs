using System.Text.Json.Serialization;

namespace music_manager.Models;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Each Artist has many Albums and many Songs.
    [JsonIgnore]
    public List<Album> Albums { get; set; } = new();
    [JsonIgnore]
    public List<Song> Songs { get; set; } = new();
}
