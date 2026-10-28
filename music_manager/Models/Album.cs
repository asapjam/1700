using System.Text.Json.Serialization;

namespace music_manager.Models;

public class Album
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ReleaseYear { get; set; }

    // Each Album has one Artist.
    public int ArtistId { get; set; }
    public Artist? Artist { get; set; }

    // Each Album has many Songs.
    [JsonIgnore]
    public List<Song> Songs { get; set; } = new();
}
