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

    //     modelBuilder.Entity<Category>().HasData(
    //         new Category { Id = 1, Name = "Uncategorized" },
    //         new Category { Id = 2, Name = "Science Fiction" },
    //         new Category { Id = 3, Name = "Fantasy" },
    //         new Category { Id = 4, Name = "Mystery" }
    //     );
    // }
}
