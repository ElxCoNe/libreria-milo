using Microsoft.EntityFrameworkCore;
using LibreriaMilo.Models;
    
namespace LibreriaMilo.Data;

public class MysqlDbContext : DbContext
{
    public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email)
                .IsUnique();
        });

        // conversión del enum a string
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(b => b.Status)
                .HasConversion<string>();
        });

        // conversión del enum a string
        modelBuilder.Entity<Loan>(entity =>
        {
            entity.Property(l => l.Status)
                .HasConversion<string>();
        });
    }
    
}