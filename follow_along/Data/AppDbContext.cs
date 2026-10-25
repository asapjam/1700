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
}
