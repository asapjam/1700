using System.Text.Json.Serialization;

namespace music_manager.Models;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }

    // Each Song has one Artist.
    public int ArtistId { get; set; }
    public Artist? Artist { get; set; }

    // Each Song has one Album.
    public int AlbumId { get; set; }
    public Artist? Album { get; set; }
}
