using Microsoft.EntityFrameworkCore;
using follow_along.Models;

namespace follow_along.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Category> Categories => Set<Category>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Uncategorized" },
            new Category { Id = 2, Name = "Science Fiction" },
            new Category { Id = 3, Name = "Fantasy" },
            new Category { Id = 4, Name = "Mystery" }
        );
    }
}
