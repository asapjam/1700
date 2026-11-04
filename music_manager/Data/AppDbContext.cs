using Microsoft.EntityFrameworkCore;
using music_manager.Models;

namespace music_manager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Song> Songs => Set<Song>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Artist>().HasData(
            new Artist { Id = 1, Name = "Ludwig van Beethoven" }
        );
        modelBuilder.Entity<Album>().HasData(
            new Album { Id = 1, Name = "Beethoven: The Essentials", ReleaseYear = 2016, ArtistId = 1 }
        );
        modelBuilder.Entity<Song>().HasData(
            new Song { Id = 1, Title = "Beethoven: Fidelio, Op. 72: Overture", Genre = "Classical", ReleaseYear = 1826, ArtistId = 1, AlbumId = 1}
        );
    }
}
