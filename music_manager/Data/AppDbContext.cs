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
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);

    //     modelBuilder.Entity<Artist>().HasData(
    //         new Artist { Id = 99, Name = "Ludwig van Beethoven" }
    //     );
    //     modelBuilder.Entity<Album>().HasData(
    //         new Album { Id = 99, Name = "Beethoven: The Essentials", ReleaseYear = 2016, ArtistId = 99 }
    //     );
    //     modelBuilder.Entity<Song>().HasData(
    //         new Song { Id = 99, Title = "Beethoven: Fidelio, Op. 72: Overture", Genre = "Classical", ReleaseYear = 1826, ArtistId = 99, AlbumId = 99}
    //     );
    // }
}
